using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace ScrapeWeb.Core.Bakabt.ScrapeFilters
{
    public class TitleFilter : BakabtScrapeFilterBase
    {
        private readonly Regex _firstLineRegex = new Regex("^(.*)", RegexOptions.Compiled | RegexOptions.Multiline);

        public override bool TryMapData<TPost>(WebPage webPage, ref TPost post)
        {
            HtmlNode node = webPage.Html;
            var descriptionTitle = node.CssSelect(".description_title").FirstOrDefault();

            if (descriptionTitle != null)
            {
                var title = descriptionTitle.InnerText.Trim();
                if (!string.IsNullOrEmpty(title))
                {
                    Match m = _firstLineRegex.Match(title);
                    if (m.Success)
                    {
                        post.Title = m.Groups[0].Value.Trim();
                    }
                    
                    return true;
                }
            }

            return false;
        }
    }
}