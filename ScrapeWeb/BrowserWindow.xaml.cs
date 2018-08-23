using System;
using System.Windows;
using System.Windows.Controls;
using Awesomium.Core;
using ScrapeWeb.Core.ViewModels;

namespace ScrapeWeb
{
    public partial class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            InitializeComponent();

        }

        public void AddTab(Uri uri)
        {
            var model = new BrowserTabViewModel();
            model.SourceUri = uri;
            BrowserWindowViewModel.BrowserTabViewModels.Add(model);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var tab = (sender as Button).CommandParameter as TabItem;

            if (tab != null)
            {
                //if (BrowserWindowViewModel.BrowserTabViewModels.Count == 0)
                //{
    
                //}
                var model = (BrowserTabViewModel)tab.DataContext;
                BrowserWindowViewModel.BrowserTabViewModels.Remove(model);
            }
        }

        private void WebControl_OnTitleChanged(object sender, TitleChangedEventArgs e)
        {
            var item = (BrowserTabViewModel) BrowserTabControl.SelectedItem;
            item.Header = e.Title;
        }
    }
}