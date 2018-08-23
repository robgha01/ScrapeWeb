using System;
using System.Windows;

namespace ScrapeWeb
{
    /// <summary>
    /// Interaction logic for InputDialogWindow.xaml
    /// </summary>
    public partial class InputDialogWindow : Window
    {
        public InputDialogWindow(string question, string defaultValue = "")
        {
                InitializeComponent();
                lblQuestion.Content = question;
                txtValue.Text = defaultValue;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
                DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtValue.SelectAll();
            txtValue.Focus();
        }

        public string Value
        {
            get { return txtValue.Text; }
        }
    }
}