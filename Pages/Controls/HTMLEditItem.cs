using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;


namespace Pages.Controls
{
	public static class HtmlEditItem {
		public static IHtmlContent EditItem<TModel, TValue>(this IHtmlHelper<TModel> h, Expression<Func<TModel, TValue>> e) {
			var lab = h.LabelFor(e, new { @class = "control-label" });
			var ed = e.Body.NodeType == ExpressionType.MemberAccess && ((MemberExpression)e.Body).Member.GetCustomAttribute(typeof(DataTypeAttribute))?.ToString() == "MultilineText" ?
					h.TextAreaFor(e, new { @class = "form-control", rows = 10 }) :
					h.EditorFor(e, new { htmlAttributes = new { @class = "form-control" } });

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
	}
}