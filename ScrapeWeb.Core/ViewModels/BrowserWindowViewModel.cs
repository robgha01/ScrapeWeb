using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ScrapeWeb.Core.Annotations;

namespace ScrapeWeb.Core.ViewModels
{
    public class BrowserWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BrowserTabViewModel> _browserTabViewModels;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BrowserTabViewModel> BrowserTabViewModels
        {
            get { return _browserTabViewModels; }
            set
            {
                _browserTabViewModels = value;
                OnPropertyChanged();
            }
        }

        public BrowserWindowViewModel()
        {
            BrowserTabViewModels = new ObservableCollection<BrowserTabViewModel>();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}