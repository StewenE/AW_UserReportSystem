using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Web;

namespace AW_UserReportSystem.Models {
    public static class HTMLShowItem {
        public static IHtmlContent ShowItem<TModel, TValue>(this IHtmlHelper<TModel> h, Expression<Func<TModel, TValue>> e) {
            var label = h.LabelFor(e, new { @class = "col-sm-2 control-label" });
            var value = h.DisplayFor(e, new { @class = "col-sm-10" });

            var dtDiv = new TagBuilder("dt");
            dtDiv.AddCssClass("col-sm-2");
            dtDiv.InnerHtml.AppendHtml(label);

            var ddDiv = new TagBuilder("dd");
            ddDiv.AddCssClass("col-sm-10");

            if(IsDescription(e)) {
                ddDiv.InnerHtml.AppendHtml("");
            }
            else {
                ddDiv.InnerHtml.AppendHtml(value);
            }

            var dl = new TagBuilder("dl");
            dl.AddCssClass("row");
            dl.InnerHtml.AppendHtml(dtDiv);
            dl.InnerHtml.AppendHtml(ddDiv);

            if(IsDescription(e)) {

                var descriptionTextBox = h.TextAreaFor(e, new { @class = "form-control", rows = 10 });

                var descriptionDiv = new TagBuilder("div");
                descriptionDiv.InnerHtml.AppendHtml(descriptionTextBox);

                dl.InnerHtml.AppendHtml(descriptionDiv);
            }

            var writer = new StringWriter();
            dl.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
