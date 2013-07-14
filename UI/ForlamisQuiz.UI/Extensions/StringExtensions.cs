using System.Globalization;

namespace ForlamisQuiz.UI.Extensions
{
    public static class StringExtensions
    {
        public static string ToLowerTurkish(this string text)
        {
            return text.ToLower(CultureInfo.GetCultureInfo("tr-TR"));
        }
    }
}