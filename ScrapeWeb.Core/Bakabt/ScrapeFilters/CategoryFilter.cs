using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt.ScrapeFilters
{
    public class CategoryFilter : BakabtScrapeFilterBase
    {
        public override bool TryMapData<TPost>(WebPage webPage, ref TPost post)
        {
            HtmlNode node = webPage.Html;
            var descriptionTitle = node.CssSelect(".description_title").FirstOrDefault();

            if (descriptionTitle != null)
            {
                var icons = descriptionTitle.CssSelect(".icon").ToList();

                foreach (var icon in icons)
                {
                    var category = icon.Attributes["title"].Value;
                    post.Categories.Add(category);
                }

                return true;
            }

            return false;
        }
    }
}