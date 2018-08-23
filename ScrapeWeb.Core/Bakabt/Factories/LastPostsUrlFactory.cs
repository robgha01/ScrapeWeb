using System.Collections.Generic;
using System.Xml.Linq;
using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.MediaManager.Abstraction;

namespace ScrapeWeb.Core.Bakabt.Factories
{
    public class LastPostsUrlFactory : PostUrlFactory
    {
        public IMediaManager MediaManager { get; set; }

        public LastPostsUrlFactory(IMediaManager mediaManager)
        {
            MediaManager = mediaManager;
        }
        public override IEnumerable<string> Create(string sourceUrl)
        {
            var postUrls = new List<string>();

            var document = MediaManager.GetDocument(sourceUrl);

            if (document.Root != null)
            {
                var ns = document.Root.Name.Namespace;

                foreach (XElement item in document.Root.Elements(ns + "channel").Elements(ns + "item"))
                {
                    // Only the link is important as we let the rest of data collecting to the scrapeManager.
                    var linkElement = item.Element(ns + "link");
                    if (linkElement == null)
                    {
                        continue;
                    }

                    postUrls.Add(linkElement.Value);
                }
            }

            return postUrls;
        }
    }
}
