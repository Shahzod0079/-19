using System.Windows;
using System.Windows.Controls;
using Практическая_19.Classes;
using Практическая_19.Modell;

namespace Практическая_19.Pages.Kinoteatr.Items
{
    public partial class Add : Page
    {
        KinoteatrContext context;
        Kinoteatr currentKinoteatr;
        bool isEdit = false;

        public Add()
        {
            InitializeComponent();
            context = new KinoteatrContext();
        }

        public Add(Kinoteatr kin)
        {
            InitializeComponent();
            context = new KinoteatrContext();
            currentKinoteatr = kin;
            isEdit = true;

            tbTitle.Text = "Редактирование кинотеатра";
            txtName.Text = kin.Name;
            txtHalls.Text = kin.HallCount.ToString();
            txtSeats.Text = kin.SeatCount.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }

            if (!int.TryParse(txtHalls.Text, out int halls) || halls <= 0)
            {
                MessageBox.Show("Введите корректное количество залов");
                return;
            }

            if (!int.TryParse(txtSeats.Text, out int seats) || seats <= 0)
            {
                MessageBox.Show("Введите корректное количество мест");
                return;
            }

            if (isEdit)
            {
                currentKinoteatr.Name = txtName.Text;
                currentKinoteatr.HallCount = halls;
                currentKinoteatr.SeatCount = seats;
                context.Update(currentKinoteatr);
            }
            else
            {
                Kinoteatr k = new Kinoteatr
                {
                    Name = txtName.Text,
                    HallCount = halls,
                    SeatCount = seats
                };
                context.Add(k);
            }

            NavigationService.GoBack();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}