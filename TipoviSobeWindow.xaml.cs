using NoviHotelWPF.DAOs;
using NoviHotelWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoviHotelWPF
{
    /// <summary>
    /// Interaction logic for TipoviSobeWindow.xaml
    /// </summary>
    public partial class TipoviSobeWindow : Window, INotifyPropertyChanged
    {
        private bool isButtonEnabled;

        public bool IsButtonEnabled
        {
            get { return isButtonEnabled; }
            set
            {
                isButtonEnabled = value;
                OnPropertyChanged("IsButtonEnabled");
            }
        }

        public TipoviSobeWindow()
        {
            InitializeComponent();
            dgTipoviSobe.ItemsSource = Aplikacija.Instanca.hotel.tipoviSobe;
            dgTipoviSobe.AutoGenerateColumns = true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            IzmeniTipSobeWindow itsw = new IzmeniTipSobeWindow(this, IzmeniTipSobeWindow.STANJE.DODAVANJE);
            itsw.Show();
            this.Hide();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            IzmeniTipSobeWindow itsw = new IzmeniTipSobeWindow(this, IzmeniTipSobeWindow.STANJE.IZMENA);
            itsw.Show();
            this.Hide();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni???", "Brisanje tipa sobe", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TipSobe ts = dgTipoviSobe.SelectedItem as TipSobe;
                Aplikacija.Instanca.hotel.tipoviSobe.Remove(ts);
                TipSobeDAO.Delete(ts);
                dgTipoviSobe.Items.Refresh();
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            SobeWindow sw = new SobeWindow();
            sw.Show();
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            MeniAdministratorWindow meniAdmin = new MeniAdministratorWindow();
            meniAdmin.Show();
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void dgTipoviSobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsButtonEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SobeWindow sw = new SobeWindow();
            sw.Show();

        }
    }
}
