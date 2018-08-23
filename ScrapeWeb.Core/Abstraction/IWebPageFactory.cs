using ScrapySharp.Network;

namespace ScrapeWeb.Core.Abstraction
{
    public interface IWebPageFactory
    {
        WebPage GetPage(string url);
    }
}