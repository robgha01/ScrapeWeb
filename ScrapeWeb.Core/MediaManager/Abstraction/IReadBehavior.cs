using System.Xml.Linq;

namespace ScrapeWeb.Core.MediaManager.Abstraction
{
    public interface IReadBehavior
    {
        XDocument Read(string url); 
    }
}