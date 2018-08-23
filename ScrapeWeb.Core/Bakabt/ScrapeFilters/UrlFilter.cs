using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt.ScrapeFilters
{
    public class UrlFilter : BakabtScrapeFilterBase
    {
        public override bool TryMapData<TPost>(WebPage webPage, ref TPost post)
        {
            post.Url = webPage.AbsoluteUrl.AbsoluteUri;

            return true;
        }
    }
}