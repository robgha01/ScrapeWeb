using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ScrapeWeb.Core.Annotations;

namespace ScrapeWeb.Core.ViewModels
{
    public class BrowserTabViewModel : INotifyPropertyChanged
    {
        private Uri _source;
        private string _header;

        public Uri SourceUri
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
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
