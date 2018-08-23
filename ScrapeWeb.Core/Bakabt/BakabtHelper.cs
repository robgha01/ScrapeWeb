using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt
{
    public static class BakabtHelper
    {
        public static Regex IframeRegex = new Regex("<iframe id=\"description_iframe\".+><\\/iframe>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static string GetDescriptionIframeUrl(WebPage webPage)
        {
            var descriptionIframeMatch = IframeRegex.Match(webPage.Content);
            if (descriptionIframeMatch.Success)
            {
                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(descriptionIframeMatch.Groups[0].Value);

                var url = webPage.AbsoluteUrl.GetLeftPart(UriPartial.Authority);
                var iframeUrl = html.GetElementbyId("description_iframe").Attributes["src"].Value;

                if (iframeUrl.StartsWith("/"))
                {
                    return url + iframeUrl;
                }

                return url + "/" + iframeUrl;
            }

            return String.Empty;
        }
    }
}