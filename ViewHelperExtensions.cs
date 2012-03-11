using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Orchard.ContentManagement;

namespace OrchardHUN.Shoutbox
{
    public static class HtmlHelperExtensions
    {
        public static string ShoutboxMessageContainerId(this HtmlHelper html, IContent shoutbox)
        {
            return "orchardhun-shoutbox-messagecontainer-" + shoutbox.Id;
        }

        public static string ShoutboxFormId(this HtmlHelper html, IContent shoutbox)
        {
            return "orchardhun-shoutbox-form--" + shoutbox.Id;
        }
    }

    public static class UrlHelperExtensions
    {
        public static string FetchMessagesUrl(this UrlHelper url, IContent shoutbox)
        {
            return url.Action("FetchMessages", new { Controller = "Shoutbox", Area = "OrchardHUN.Shoutbox", ShoutboxId = shoutbox.Id });
        }
    }
}
