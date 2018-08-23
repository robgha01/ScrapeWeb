using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.Bakabt.Abstraction;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt.ScrapeFilters
{
    public abstract class BakabtScrapeFilterBase : IScrapeFilter, IInformationType
    {
        public virtual InformationType InformationType { get { return InformationType.None; } }

        public abstract bool TryMapData<TPost>(WebPage webPage, ref TPost post) where TPost : IPost;
    }
}
