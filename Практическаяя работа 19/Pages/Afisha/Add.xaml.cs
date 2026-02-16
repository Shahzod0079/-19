using System;
using System.Windows;
using System.Windows.Controls;
using Практическая_19.Classes;
using Практическая_19.Modell;

namespace Практическая_19.Pages.Afisha.Items
{
    public partial class Add : Page
    {
        AfishaContext afishaContext;
        KinoteatrContext kinoteatrContext;
        Afisha currentAfisha;
        bool isEdit = false;

        public Add()
        {
            InitializeComponent();
            afishaContext = new AfishaContext();
            kinoteatrContext = new KinoteatrContext();
        }

        public Add(Afisha af)
        {
            InitializeComponent();
            afishaContext = new AfishaContext();
            kinoteatrContext = new KinoteatrContext();
            currentAfisha = af;
            isEdit = true;

            tbTitle.Text = "Редактирование афиши";
            txtFilm.Text = af.FilmName;
            dpDate.SelectedDate = af.SessionTime.Date;
            txtTime.Text = af.SessionTime.ToString("HH:mm");
            txtPrice.Text = af.TicketPrice.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbKinoteatrs.ItemsSource = kinoteatrContext.GetAll();

            if (isEdit && currentAfisha != null)
            {
                cmbKinoteatrs.SelectedValue = currentAfisha.KinoteatrId;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKinoteatrs.SelectedValue == null)
            {
                MessageBox.Show("Выберите кинотеатр");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFilm.Text))
            {
                MessageBox.Show("Введите название фильма");
                return;
            }

            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену");
                return;
            }

            // Собираем дату и время
            DateTime sessionTime = dpDate.SelectedDate.Value;
            string[] timeParts = txtTime.Text.Split(':');
            if (timeParts.Length == 2 && int.TryParse(timeParts[0], out int hour) && int.TryParse(timeParts[1], out int minute))
            {
                sessionTime = sessionTime.AddHours(hour).AddMinutes(minute);
            }

            if (isEdit)
            {
                currentAfisha.KinoteatrId = (int)cmbKinoteatrs.SelectedValue;
                currentAfisha.FilmName = txtFilm.Text;
                currentAfisha.SessionTime = sessionTime;
                currentAfisha.TicketPrice = price;
                afishaContext.Update(currentAfisha);
            }
            else
            {
                Afisha a = new Afisha
                {
                    KinoteatrId = (int)cmbKinoteatrs.SelectedValue,
                    FilmName = txtFilm.Text,
                    SessionTime = sessionTime,
                    TicketPrice = price
                };
                afishaContext.Add(a);
            }

            NavigationService.GoBack();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}