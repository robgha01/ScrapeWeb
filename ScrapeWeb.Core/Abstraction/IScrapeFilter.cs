using ScrapySharp.Network;

namespace ScrapeWeb.Core.Abstraction
{
    public interface IScrapeFilter
    {
        bool TryMapData<TPost>(WebPage webPage, ref TPost post) where  TPost : IPost;
    }
}