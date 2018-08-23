using System.Xml.Linq;
using ScrapeWeb.Core.MediaManager.Abstraction;

namespace ScrapeWeb.Core.MediaManager
{
    public abstract class MediaManagerBase : IMediaManager
    {
        protected IReadBehavior ReadBehavior;
        
        protected void SetReadBehavior(IReadBehavior readBehavior)
        {
            ReadBehavior = readBehavior;
        }

        public XDocument GetDocument(string url)
        {
            return ReadBehavior.Read(url);
        }
    }
}