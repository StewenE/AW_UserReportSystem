using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;


namespace Pages.Controls {
    public static class HtmlEditItem {
        public static IHtmlContent EditItem<TModel, TValue>(this IHtmlHelper<TModel> h, Expression<Func<TModel, TValue>> e) {
            var lab = h.LabelFor(e, new { @class = "control-label" });
            var ed = h.EditorFor(e, new { htmlAttributes = new { @class = "form-control" } });
            var val = h.ValidationMessageFor(e, string.Empty, new { @class = "text-danger" });


            var div = new TagBuilder("div");
            div.AddCssClass("form-group");
            div.InnerHtml.AppendHtml(lab);
            div.InnerHtml.AppendHtml(ed);
            div.InnerHtml.AppendHtml(val);

            var writer = new StringWriter();
            div.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
        public static IHtmlContent ShowItem<TModel, TValue>(this IHtmlHelper<TModel> h, Expression<Func<TModel, TValue>> e) {

            var label = h.LabelFor(e, new { @class = "col-sm-2 control-label" });
            var value = h.DisplayFor(e, new { @class = "col-sm-10" });

            var dtDiv = new TagBuilder("dt");
            dtDiv.AddCssClass("col-sm-2");
            dtDiv.InnerHtml.AppendHtml(label);

            var ddDiv = new TagBuilder("dd");
            ddDiv.AddCssClass("col-sm-10");
            ddDiv.InnerHtml.AppendHtml(value);

            var dl = new TagBuilder("dl");
            dl.AddCssClass("row");
            dl.InnerHtml.AppendHtml(dtDiv);
            dl.InnerHtml.AppendHtml(ddDiv);

            var writer = new StringWriter();
            dl.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());

        }
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

        private static TagBuilder createBody<TModel>(this IHtmlHelper<IEnumerable<TModel>> h,
            PropertyInfo[] properties, IEnumerable<TModel> items) where TModel : Entity {
            var tbody = new TagBuilder("tbody");

            foreach(var i in items) {
                var tr = new TagBuilder("tr");
                TagBuilder td;
                foreach(var p in properties) {
                    td = new TagBuilder("td");
                    var v = p?.GetValue(i)?.ToString() ?? string.Empty;
                    var value = h.Raw(v);
                    td.InnerHtml.AppendHtml(value);
                    tr.InnerHtml.AppendHtml(td);
                }
                var id = i?.Id.ToString() ?? string.Empty;
                td = new TagBuilder("td");
                h.addLink("Edit", id, td);
                h.addLink("Details", id, td);
                h.addLink("Delete", id, td, true);
                tr.InnerHtml.AppendHtml(td);
                tbody.InnerHtml.AppendHtml(tr);
            }

            return tbody;
        }
        private static void addLink<TModel>(this IHtmlHelper<IEnumerable<TModel>> h, string action, string id, TagBuilder td, bool isLast = false) {
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