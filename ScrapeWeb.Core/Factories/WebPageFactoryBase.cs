using System;
using System.Runtime.Caching;
using ScrapeWeb.Core.Abstraction;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Factories
{
    public abstract class WebPageFactoryBase : IWebPageFactory
    {
        protected const string PostCacheKey = "PostUrl";

        public abstract WebPage GetPage(string url);
        
        protected WebPage TryGetFromCache(string url)
        {
            //Get the default MemoryCache to cache objects in memory
            ObjectCache cache = MemoryCache.Default;
            var cacheKey = PostCacheKey + url;

            if (cache.Contains(cacheKey))
            {
                return cache[cacheKey] as WebPage;
            }

            //Create a custom Timeout of 10 seconds
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromMinutes(10);

            var browser = new ScrapingBrowser();
            var webPage = browser.NavigateToPage(new Uri(url));

            //add the object to the cache
            cache.Add(cacheKey, webPage, policy);

            return webPage;
        }
    }
}