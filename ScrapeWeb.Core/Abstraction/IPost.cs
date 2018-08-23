using System.Collections.Generic;
using System.ComponentModel;

namespace ScrapeWeb.Core.Abstraction
{
    public interface IPost : INotifyPropertyChanged
    {
        string Url { get; set; }
        string Title { get; set; }
        byte[] Cover { get; set; }
        IList<string> Categories { get; set; }
        IList<string> Generes { get; set; }
        void SaveCover(string path);
    }
}