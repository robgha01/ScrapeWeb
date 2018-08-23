using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using ExCSS;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt.ScrapeFilters
{
    public class CoverFilter : BakabtScrapeFilterBase
    {
        protected Regex CssImageUrlRegex = new Regex("url\\([\\\"\']?(.*?)[\"\']?\\)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        protected Regex BakaShotsRegex = new Regex(@"\b(?:https?://|www\.)bakashots\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public override InformationType InformationType { get { return InformationType.DescriptionIframe; } }

        public override bool TryMapData<TPost>(WebPage webPage, ref TPost post)
        {
            string coverUrl;
            HtmlNode node = webPage.Html;
            var cover = node.CssSelect(".cover").FirstOrDefault();

            if (cover != null)
            {
                coverUrl = cover.Attributes["src"].Value;
            }
            else
            {
                // The cover was not found try to look in the style elements
                coverUrl = GetCoverFromStyleElement(webPage);

                if (string.IsNullOrEmpty(coverUrl))
                {
                    coverUrl = GetFirstBakashotImage(webPage);
                }
            }

            if (!string.IsNullOrEmpty(coverUrl))
            {
                // ToDo: Download image and save it as bits or to file ?
                //WebClient client = new WebClient();
                
                //try
                //{
                //    //var imageBytes = client.DownloadData(coverUrl);
                //    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
                //    client.Headers.Add("Referer", post.Url);


                //    post.Cover = client.DownloadData(coverUrl);

                //    return true;
                //}
                //catch (WebException we)
                //{
                //    // Failed to load image for some reason
                //}
                byte[] coverBytes;
                var isSucess = TryGetFromCache(coverUrl, out coverBytes);
                post.Cover = coverBytes;

                return isSucess;
            }

            //Load default image
            //string appBaseDir = AppDomain.CurrentDomain.BaseDirectory;
            //post.Cover = System.IO.File.ReadAllBytes(appBaseDir + "\\no_cover.jpg");

            return false;
        }

        protected bool TryGetFromCache(string coverUrl, out byte[] coverBytes)
        {
            //Get the default MemoryCache to cache objects in memory
            ObjectCache cache = MemoryCache.Default;
            var cacheKey = "cover" + coverUrl;
            var noCoverCacheKey = "no_cover";

            if (cache.Contains(cacheKey))
            {
                coverBytes = cache[cacheKey] as byte[];
                return true;
            }

            //Create a custom Timeout of 10 seconds
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromMinutes(10);
            
            try
            {
                WebClient client = new WebClient();
                //var imageBytes = client.DownloadData(coverUrl);
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
                //client.Headers.Add("Referer", post.Url);
                
                coverBytes = client.DownloadData(coverUrl);
                //add the object to the cache
                cache.Add(cacheKey, coverBytes, policy);

                return true;
            }
            catch (WebException we)
            {
                // Failed to load image for some reason
            }

            if (cache.Contains(noCoverCacheKey))
            {
                coverBytes = cache[noCoverCacheKey] as byte[];
                return false;
            }

            //Load default image
            string appBaseDir = AppDomain.CurrentDomain.BaseDirectory;
            coverBytes = System.IO.File.ReadAllBytes(appBaseDir + "\\no_cover.jpg");
            cache.Add(noCoverCacheKey, coverBytes, policy);

            return false;
        }

        protected string GetCoverFromStyleElement(WebPage webPage)
        {
            HtmlNode node = webPage.Html;

            // It may be embeded in a style element...
            var styleNodes = node.Descendants("style");

            foreach (var styleNode in styleNodes)
            {
                var parser = new Parser();
                var stylesheet = parser.Parse(styleNode.InnerText);

                var backgroundImage = stylesheet.StyleRules
                                                .SelectMany(r => r.Declarations)
                                                .FirstOrDefault(d => d.Name.Equals("background-image", StringComparison.InvariantCultureIgnoreCase) || d.Name.Equals("background", StringComparison.InvariantCultureIgnoreCase) && !(d.Term is HtmlColor));

                if (backgroundImage != null)
                {
                    var imageUrl = backgroundImage
                        .Term
                        .ToString();

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        continue;
                    }

                    var matches = CssImageUrlRegex.Matches(imageUrl);

                    foreach (Match match in matches)
                    {
                        var url = match.Groups[1].Value;


                        return url;
                    }

                    return imageUrl;
                }
            }

            return String.Empty;
        }

        protected string GetFirstBakashotImage(WebPage webPage)
        {
            var urls = webPage.Html.Descendants("img")
                                .Select(e => e.GetAttributeValue("src", null))
                                .Where(s => !String.IsNullOrEmpty(s) && s.Contains("://bakashots"));

            var url = urls.FirstOrDefault();

            if (url == null)
            {
                // Text search for bakashots image...
                var matches = BakaShotsRegex.Matches(webPage.Html.InnerHtml);
                if (matches.Count >= 1)
                {
                    // Random image from images
                    url = matches[new Random().Next(0, matches.Count)].Value;

                    //url = matches[0].Value;
                }
            }

            return url;
        }
    }
}