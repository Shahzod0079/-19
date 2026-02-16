using System.Windows;
using System.Windows.Controls;
using Практическая_19.Classes;
using Практическая_19.Modell;
using Практическая_19.Pages.Kinoteatr.Items;

namespace Практическая_19.Pages.Kinoteatr
{
    public partial class Main : Page
    {
        KinoteatrContext context;

        public Main()
        {
            InitializeComponent();
            context = new KinoteatrContext();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgKinoteatrs.ItemsSource = context.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgKinoteatrs.SelectedItem is Kinoteatr kin)
            {
                NavigationService.Navigate(new Add(kin));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgKinoteatrs.SelectedItem is Kinoteatr kin)
            {
                if (MessageBox.Show("Удалить?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    context.Delete(kin.Id);
                    LoadData();
                }
            }
        }
    }
}