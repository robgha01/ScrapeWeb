using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AlphaChiTech.Virtualization;
using Awesomium.Core;
using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.Bakabt;
using ScrapeWeb.Core.Factories;
using ScrapeWeb.Properties;

namespace ScrapeWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected bool HasCoverSize;
        private BrowserWindow _browserWindow;
        
        protected BrowserWindow BrowserWindow
        {
            get
            {
                if (_browserWindow == null)
                {
                    _browserWindow = new BrowserWindow();
                }

                return _browserWindow;
            }
            set { _browserWindow = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            
            //this routine only needs to run once, so first check to make sure the
            //VirtualizationManager isn’t already initialized
            if (!VirtualizationManager.IsInitialized)
            {
                //set the VirtualizationManager’s UIThreadExcecuteAction. In this case
                //we’re using Dispatcher.Invoke to give the VirtualizationManager access
                //to the dispatcher thread, and using a DispatcherTimer to run the background
                //operations the VirtualizationManager needs to run to reclaim pages and manage memory.
                VirtualizationManager.Instance.UIThreadExcecuteAction = 
                    (a) => Dispatcher.Invoke(a);
                new DispatcherTimer(
                    TimeSpan.FromSeconds(1),
                    DispatcherPriority.Background,
                    delegate(object s, EventArgs a)
                    {
                        VirtualizationManager.Instance.ProcessActions();
                    },
                    Dispatcher).Start();
            }

            BrowserWindow.Closing += BrowserWindowOnClosing;
            MainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LastPosts")
            {
                try
                {
                    if (MainWindowViewModel.LastPosts == null)
                    {
                        return;
                    }

                    var categories = MainWindowViewModel.LastPosts.Where(x => x != null).Select(x => x.Categories).SelectMany(x => x).Distinct().ToList();
                    MainWindowViewModel.Categories = new MultiSelectCollectionView<string>(categories);

                    var generes = MainWindowViewModel.LastPosts.Where(x => x != null).Select(x => x.Generes).SelectMany(x => x).Distinct().ToList();
                    MainWindowViewModel.Generes = new MultiSelectCollectionView<string>(generes);
                }
                catch (Core.MediaManager.Exceptions.RssException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BrowserWindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            cancelEventArgs.Cancel = true;
            BrowserWindow.Visibility = Visibility.Collapsed;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.LastPostsRssUrl = Settings.Default.RssUrl;

            Window mainWindow = Application.Current.MainWindow;
            PresentationSource mainWindowPresentationSource = PresentationSource.FromVisual(mainWindow);
            if (mainWindowPresentationSource != null)
            {
                if (mainWindowPresentationSource.CompositionTarget != null)
                {
                    Matrix m = mainWindowPresentationSource.CompositionTarget.TransformToDevice;
                    var dpiWidthFactor = m.M11;
                    var dpiHeightFactor = m.M22;
                    double screenHeight = SystemParameters.PrimaryScreenHeight * dpiHeightFactor;
                    double screenWidth = SystemParameters.PrimaryScreenWidth * dpiWidthFactor;

                    Application.Current.MainWindow.MaxWidth = screenWidth - (screenWidth * 0.10);
                    Application.Current.MainWindow.MaxHeight = screenHeight - (screenHeight * 0.10);
                }
            }
        }
        
        private void Contents_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (HasCoverSize)
            {
                return;
            }

            var control = (FrameworkElement)e.Source;

            // This is the container
            var parent = (Panel)control.Parent;
            var cover = parent.Children.OfType<Image>().First();

            if (cover.Source == null)
            {
                return;
            }

            var newHight = control.ActualHeight + cover.ActualHeight;

            // Check if the new hight is lower then the application height, if true set it to current height.
            if (newHight < MainBord.ActualHeight)
            {
                newHight = MainBord.ActualHeight;
            }

            MainBord.MaxHeight = newHight;

            HasCoverSize = true;
        }

        private void LastPostsListView_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (IPost)LastPostsListView.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }

            BrowserWindow.AddTab(selectedItem.Url.ToUri());

            if (BrowserWindow.Visibility == Visibility.Collapsed)
            {
                BrowserWindow.Visibility = Visibility.Visible;
            }

            if (!BrowserWindow.IsLoaded)
            {
                BrowserWindow.Show();
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialogWindow("Enter your bakabt rss url:", Settings.Default.RssUrl ?? String.Empty);
            if (inputDialog.ShowDialog() == true)
            {
                Settings.Default.RssUrl = inputDialog.Value;
                MainWindowViewModel.LastPostsRssUrl = inputDialog.Value;
                MainWindowViewModel.LoadLastPosts();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Settings.Default.Save();
            Application.Current.Shutdown();
        }
    }
}
