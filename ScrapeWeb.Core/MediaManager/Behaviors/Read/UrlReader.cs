using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.MediaManager.Abstraction;
using ScrapeWeb.Core.MediaManager.Exceptions;

namespace ScrapeWeb.Core.MediaManager.Behaviors.Read
{
    public class UrlReader : IReadBehavior
    {
        protected IWebPageFactory WebPageFactory;

        public UrlReader(IWebPageFactory webPageFactory)
        {
            WebPageFactory = webPageFactory;
        }

        public virtual XDocument Read(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NoNullAllowedException("Url is null or emtpy!");
            }

            var webPage = WebPageFactory.GetPage(url);
            XDocument document = XDocument.Parse(webPage);
            //Error
            if (document.Root != null)
            {
                var ns = document.Root.Name.Namespace;
                var firstItem = document.Descendants("item").FirstOrDefault();
                if (firstItem != null)
                {
                    var titleElement = firstItem.Element(ns + "title");
                    if (titleElement.Value.Equals("error", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Someting is wrong with the rss url...
                        throw new RssException("Someting is wrong, check if the rss url needs a update.");
                    }
                }
            }

            return document;
        }
    }
}