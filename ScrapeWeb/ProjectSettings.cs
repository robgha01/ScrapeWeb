using System.ComponentModel;
using System.Runtime.CompilerServices;
using ScrapeWeb.Core.Annotations;
using ScrapeWeb.Properties;

namespace ScrapeWeb
{
    public class ProjectSettings : INotifyPropertyChanged
    {
        public string RssUrl
        {
            get { return Settings.Default.RssUrl; }
            set
            {
                Settings.Default.RssUrl = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}