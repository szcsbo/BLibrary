using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlExtension
    {
        public static MvcHtmlString Sample(this HtmlHelper htmlHelper, string parm)
        {
            string html = string.Empty;
            return new MvcHtmlString(html);
        }
    }
}