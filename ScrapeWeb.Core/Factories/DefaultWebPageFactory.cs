using ScrapySharp.Network;

namespace ScrapeWeb.Core.Factories
{
    public class DefaultWebPageFactory : WebPageFactoryBase
    {
        public override WebPage GetPage(string url)
        {
            return TryGetFromCache(url);
        }
    }
}