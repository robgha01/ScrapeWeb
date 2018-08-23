using System.Collections.Generic;
using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.Bakabt.ScrapeFilters;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt
{
    public class BakaBtScrapeManager : IScrapeManager<PostEntity, BakabtScrapeFilterBase>
    {
        protected List<BakabtScrapeFilterBase> ScrapeFilters = new List<BakabtScrapeFilterBase>();
        public IWebPageFactory WebPageFactory { get; set; }

        public BakaBtScrapeManager(IWebPageFactory webPageFactory)
        {
            WebPageFactory = webPageFactory;

            // ToDo: Move this to Castle.Core lookup
            ScrapeFilters.Add(new UrlFilter());
            ScrapeFilters.Add(new CategoryFilter());
            ScrapeFilters.Add(new CoverFilter());
            ScrapeFilters.Add(new TitleFilter());
            ScrapeFilters.Add(new GeneresFilter());
        }

        public IScrapeManager<PostEntity, BakabtScrapeFilterBase> AddFilter(BakabtScrapeFilterBase filter)
        {
            ScrapeFilters.Add(filter);

            return this;
        }

        public PostEntity Process(string url)
        {
            var entity = new PostEntity();
            
            foreach (var filter in ScrapeFilters)
            {
                WebPage webPage = WebPageFactory.GetPage(url);
                
                switch (filter.InformationType)
                {
                    case InformationType.DescriptionIframe:
                        var iframeUrl = BakabtHelper.GetDescriptionIframeUrl(webPage);
                        if (!string.IsNullOrEmpty(iframeUrl))
                        {
                            webPage = WebPageFactory.GetPage(iframeUrl);
                        }
                        break;
                }

                filter.TryMapData(webPage, ref entity);
            }

            return entity;
        }
    }
}