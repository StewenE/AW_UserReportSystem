using System.Reflection;
using System.Text.Encodings.Web;
using System.Web;

namespace AW_UserReportSystem.Models {
    public static class HTMLShowTable {
        public static IHtmlContent ShowTable<TModel>(this IHtmlHelper<IEnumerable<TModel>> h,
            IEnumerable<TModel> items) where TModel : Entity {

            var table = new TagBuilder("table");
            table.AddCssClass("table");

            var properties = getProperties(typeof(TModel));

            var thead = h.createHead(properties);
            table.InnerHtml.AppendHtml(thead);

            var body = h.createBody(properties, items);
            table.InnerHtml.AppendHtml(body);

            var writer = new StringWriter();
            table.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        private static TagBuilder createHead<TModel>(this IHtmlHelper<IEnumerable<TModel>> h, PropertyInfo[] properties) {
            var thead = new TagBuilder("thead");
            var tr = new TagBuilder("tr");
            foreach(var p in properties) h.addColumn(tr, p.Name);

            h.addColumn(tr, string.Empty);
            thead.InnerHtml.AppendHtml(tr);
            return thead;
        }

        private static TagBuilder createBody<TModel>(this IHtmlHelper<IEnumerable<TModel>>
            h, PropertyInfo[] properties, IEnumerable<TModel> items) where TModel : Entity {

            var tbody = new TagBuilder("tbody");

            var sortedItems = items.OrderBy(item => {
                var solveByDate = (DateTime)item.GetType().GetProperty("SolveByDate").GetValue(item);
                var timeDifference = solveByDate - DateTime.Now;
                return timeDifference;
            });

            foreach(var item in sortedItems) {
                var tr = new TagBuilder("tr");
                var solveByDate = (DateTime)item.GetType().GetProperty("SolveByDate").GetValue(item);
                var timeDifference = solveByDate - DateTime.Now;
                if(timeDifference.TotalHours < 1) {
                    tr.AddCssClass("table-danger");
                }
                TagBuilder td;

                foreach(var property in properties) {
                    td = new TagBuilder("td");
                    var value = property?.GetValue(item)?.ToString() ?? string.Empty;
                    if(property.Name == "Description") {
                        value = value.Length > 50 ? value.Substring(0, 50) + "..." : value;
                    }
                    var valueHtml = h.Raw(value);
                    td.InnerHtml.AppendHtml(valueHtml);
                    tr.InnerHtml.AppendHtml(td);
                }
                var id = item?.Id.ToString() ?? string.Empty;
                td = new TagBuilder("td");
                h.addLink("Details", id, td);
                h.addLink("Solved", id, td, true);
                tr.InnerHtml.AppendHtml(td);
                tbody.InnerHtml.AppendHtml(tr);
            }

            return tbody;
        }
        private static void addLink<TModel>(this IHtmlHelper<IEnumerable<TModel>>
            h, string action, string id, TagBuilder td, bool isLast = false) {
            var link = h.ActionLink(action, action, new { Id = id });
            td.InnerHtml.AppendHtml(link);
            if(isLast) return;
            td.InnerHtml.AppendHtml(new HtmlString(" | "));
        }
        private static void addColumn<TModel>(this IHtmlHelper<IEnumerable<TModel>> h,
            TagBuilder tr, string value, string tag = "th") {
            var th = new TagBuilder(tag);
            var v = h.Raw(value);
            th.InnerHtml.AppendHtml(v);
            tr.InnerHtml.AppendHtml(th);
        }
        private static PropertyInfo[] getProperties(Type type) => type?.GetProperties()?.Where(x => x.Name != "Id")?.ToArray() ?? [];
    }
}
}
