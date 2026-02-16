using System.Windows;
using Практическая_19.Pages.Kinoteatr;
using Практическая_19.Pages.Afisha;

namespace Практическая_19
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new KinoteatrMain());
        }

        private void Kinoteatr_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new KinoteatrMain());
        }

        private void Afisha_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AfishaMain());
        }
    }
}