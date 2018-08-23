using ScrapeWeb.Core.Factories;
using ScrapeWeb.Core.MediaManager.Behaviors.Read;

namespace ScrapeWeb.Core.MediaManager
{
    public class MediaManager : MediaManagerBase
    {
        public MediaManager()
        {
            SetReadBehavior(new UrlReader(new DefaultWebPageFactory()));
        }
    }
}
