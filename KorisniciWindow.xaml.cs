using NoviHotelWPF.DAOs;
using NoviHotelWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for KorisniciWindow.xaml
    /// </summary>
    public partial class KorisniciWindow : Window, INotifyPropertyChanged
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

        public KorisniciWindow()
        {
            InitializeComponent();
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.korisnici);
            view.SortDescriptions.Add(new SortDescription("korisnickoIme", ListSortDirection.Descending));

            dgKorisnici.ItemsSource = view;
            dgKorisnici.AutoGenerateColumns = false;
            this.DataContext = this;

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            IzmeniKorisnikaWindow ikw = new IzmeniKorisnikaWindow(this, IzmeniKorisnikaWindow.STANJE.DODAVANJE);
            ikw.Show();
            this.Hide();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni???", "Brisanje korisnika", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Korisnik k = dgKorisnici.SelectedItem as Korisnik;
                Aplikacija.korisnici.Remove(k);
                KorisnikDAO.DeleteLogicko(k);
                dgKorisnici.Items.Refresh();
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            IzmeniKorisnikaWindow ikw = new IzmeniKorisnikaWindow(this, IzmeniKorisnikaWindow.STANJE.IZMENA);
            ikw.Show();
            this.Hide();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            //GlavniMeniWindow gmw = new GlavniMeniWindow();
            //gmw.Show();
            MeniAdministratorWindow meniAdmin = new MeniAdministratorWindow();
            meniAdmin.Show();
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            MeniAdministratorWindow meniAdmin = new MeniAdministratorWindow();
            meniAdmin.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string unetTekst = tbUneto.Text;
            if (unetTekst != "")
            {
                popuniTabeluKorisnika();
                ObservableCollection<Korisnik> filtriranaLista = new ObservableCollection<Korisnik>();
                foreach (Korisnik k in dgKorisnici.Items)
                {
                    if (k.KorisnickoIme.Contains(unetTekst))
                    {
                        filtriranaLista.Add(k);
                    }
                }
                dgKorisnici.ItemsSource = filtriranaLista;
            }
            else popuniTabeluKorisnika();
        }

        private void popuniTabeluKorisnika()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.korisnici);
            view.SortDescriptions.Add(new SortDescription("korisnickoIme", ListSortDirection.Descending));
            dgKorisnici.ItemsSource = view;
            dgKorisnici.AutoGenerateColumns = false;
        }


        private void dgKorisnici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnEdit.IsEnabled = true;
            //btnDelete.IsEnabled = true;
            this.IsButtonEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlavniMeniWindow glavniProzor = new GlavniMeniWindow();
            glavniProzor.Show();

        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
