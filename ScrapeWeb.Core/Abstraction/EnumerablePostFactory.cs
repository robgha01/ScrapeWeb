using System.Collections.Generic;

namespace ScrapeWeb.Core.Abstraction
{
    public abstract class EnumerablePostFactory
    {
        public abstract IEnumerable<IPost> CreatePosts(string url);

        public abstract IEnumerable<string> Create(string url);
    }
}