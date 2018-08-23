using System.Collections.Generic;
using System.Xml.Linq;
using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.Bakabt.ScrapeFilters;
using ScrapeWeb.Core.MediaManager.Abstraction;

namespace ScrapeWeb.Core.Bakabt.Factories
{
    //ToDo: Rework this factory to take in a list of string urls and parse them to IPosts.
    public class LastEnumerablePostsFactory : EnumerablePostFactory
    {
        public IMediaManager MediaManager { get; set; }
        public IScrapeManager<PostEntity, BakabtScrapeFilterBase> ScrapeManager { get; set; }

        public LastEnumerablePostsFactory(IMediaManager mediaManager, IScrapeManager<PostEntity, BakabtScrapeFilterBase> scrapeManager)
        {
            MediaManager = mediaManager;
            ScrapeManager = scrapeManager;
        }

        public override IEnumerable<IPost> CreatePosts(string url)
        {
            //var t = ScrapeManager.Process("http://bakabt.me/description.php?id=179973");

            var postEntities = new List<IPost>();

            //"https://bakabt.me/rss.php?uid=1825709&key=55720730a2924d8c3ddcc638caa2a84f"
            var document = MediaManager.GetDocument(url);
            
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

                    var entity = ScrapeManager.Process(linkElement.Value);
                    
                    //// ToDo: Add a default image if no cover is found.
                    if (entity.Cover != null)
                    {
                        postEntities.Add(entity);
                    }
                }
            }

            //for (int i = 0; i < 500; i++)
            //{
            //    postUrls.Add(postUrls[0]);
            //}

            return postEntities;
        }

        public override IEnumerable<string> Create(string url)
        {
            var postUrls = new List<string>();

            var document = MediaManager.GetDocument(url);

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
