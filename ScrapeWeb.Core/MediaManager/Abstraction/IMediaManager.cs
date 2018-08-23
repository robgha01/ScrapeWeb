using System.Xml.Linq;

namespace ScrapeWeb.Core.MediaManager.Abstraction
{
    public interface IMediaManager
    {
        XDocument GetDocument(string url);
    }
}