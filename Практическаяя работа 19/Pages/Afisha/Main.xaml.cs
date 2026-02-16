using System.Windows;
using System.Windows.Controls;
using Практическая_19.Classes;
using Практическая_19.Modell;
using Практическая_19.Pages.Afisha.Items;
using Практическая_19.Pages.Kinoteatr.Items;

namespace Практическая_19.Pages.Afisha
{
    public partial class Main : Page
    {
        AfishaContext context;

        public Main()
        {
            InitializeComponent();
            context = new AfishaContext();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgAfishas.ItemsSource = context.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgAfishas.SelectedItem is Afisha af)
            {
                NavigationService.Navigate(new Add(af));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAfishas.SelectedItem is Afisha af)
            {
                if (MessageBox.Show("Удалить?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    context.Delete(af.Id);
                    LoadData();
                }
            }
        }
    }
}