using System;

namespace ScrapeWeb.Core.MediaManager.Exceptions
{
    public class RssException : Exception
    {
        public RssException() {}

        public RssException(string message) : base(message) {}

        public RssException(string message, Exception innerException) : base(message, innerException) {}
    }
}