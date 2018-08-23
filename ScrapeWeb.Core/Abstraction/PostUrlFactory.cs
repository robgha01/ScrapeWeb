using System.Collections.Generic;

namespace ScrapeWeb.Core.Abstraction
{
    public abstract class PostUrlFactory
    {
        public abstract IEnumerable<string> Create(string sourceUrl);
    }
}
