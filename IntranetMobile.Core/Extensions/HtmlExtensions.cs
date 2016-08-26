using System;
using System.Text.RegularExpressions;
using AngleSharp.Parser.Html;

namespace IntranetMobile.Core.Extensions
{

    public static class HtmlExtensions
    {
        public static string GetFirstImageUri(this string body)
        {
            var parser = new HtmlParser();
            var parseObj = parser.Parse(body);
            var imageUri = string.Empty;
            if (parseObj.Images.Length > 0)
            {
                imageUri = parseObj.Images[0].Source;
            }

            return imageUri;
        }

        public static string RemoveHTMLTags(this string input)
        {
            return Regex.Replace(input.Replace("&nbsp;", " "), "<.*?>", string.Empty);
        }
    }
}
