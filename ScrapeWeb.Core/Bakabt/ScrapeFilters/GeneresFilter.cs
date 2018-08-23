using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt.ScrapeFilters
{
    public class GeneresFilter : BakabtScrapeFilterBase
    {
        public override bool TryMapData<TPost>(WebPage webPage, ref TPost post)
        {
            HtmlNode node = webPage.Html;
            var descriptionTitle = node.CssSelect(".description_title").FirstOrDefault();

            if (descriptionTitle != null)
            {
                var tags = descriptionTitle.CssSelect(".tags").FirstOrDefault();

                if (tags != null)
                {
                    var generes = tags.Elements("a");

                    foreach (var item in generes)
                    {
                        var genre = item.InnerText;
                        post.Generes.Add(genre);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}