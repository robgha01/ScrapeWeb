using System.Windows;
//using ScrapeWeb.Core;

namespace ScrapeWeb
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public ServiceLocator ServiceLocator { get; private set; }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            //ServiceLocator = new ServiceLocator();
        }


        private void App_OnExit(object sender, ExitEventArgs e)
        {
            //ServiceLocator.Dispose();
            ScrapeWeb.Properties.Settings.Default.Save();
        }
    }
}