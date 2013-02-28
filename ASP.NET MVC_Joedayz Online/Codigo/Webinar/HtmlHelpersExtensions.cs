using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webinar
{
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public static class HtmlHelpersExtensions
    {
        public static string slug(this HtmlHelper helpers, string title)
        {

            string slug = title.ToLower();

            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", ""); // caracteres invalidos      
            slug = Regex.Replace(slug, @"\s+", " ").Trim(); // spacios en blanco
            slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim(); // recortar
            slug = Regex.Replace(slug, @"\s", "-"); // colocar guiones

            return slug;
        }
    }
}