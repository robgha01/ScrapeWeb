using System;
using System.Collections.Generic;
using System.Linq;
using AlphaChiTech.Virtualization;
using ScrapeWeb.Core.Factories;

namespace ScrapeWeb.Core.Bakabt
{
    public class PostsSourceProvider : IPagedSourceProvider<PostEntity>
    {
        protected IEnumerable<string> PostUrls;

        public PostsSourceProvider(IEnumerable<string> postUrls)
        {
            PostUrls = postUrls;
        }

        public void OnReset(int count)
        {
        }

        public PagedSourceItemsPacket<PostEntity> GetItemsAt(int pageoffset, int count, bool usePlaceholder)
        {
            var posts = new List<PostEntity>();
            var scrapeManager = new BakaBtScrapeManager(new DefaultWebPageFactory());
            var urls = PostUrls.Skip(pageoffset).Take(count);
            
            foreach (var url in urls)
            {
                PostEntity post = scrapeManager.Process(url);

                // For now we exclude posts with no cover.
                if (post.Cover == null)
                {
                    continue;
                }

                posts.Add(post);
            }

            return new PagedSourceItemsPacket<PostEntity>
            {
                LoadedAt = DateTime.Now,
                Items = posts
            };
        }

        public int IndexOf(PostEntity item)
        {
            return -1;
        }

        public int Count
        {
            get { return PostUrls.Count(); }
        }
    }
}
