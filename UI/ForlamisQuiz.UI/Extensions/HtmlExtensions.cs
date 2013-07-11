using System.Web.Mvc;

namespace ForlamisQuiz.UI.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SubmitButton(this HtmlHelper helper)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes.Add("type", "submit");
            tagBuilder.Attributes.Add("value", "Gönder");

            return new MvcHtmlString(tagBuilder.ToString());
        }
    }
}