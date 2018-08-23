using ScrapeWeb.Core.Abstraction;
using ScrapySharp.Network;

namespace ScrapeWeb.Core
{
    public abstract class ScrapeFilterBase : IScrapeFilter
    {
        public abstract bool TryMapData<TPost>(WebPage webPage, ref TPost post) where TPost : IPost;
    }
}
