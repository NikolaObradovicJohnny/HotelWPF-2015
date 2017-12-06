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
    /// Interaction logic for SobeWindow.xaml
    /// </summary>
    public partial class SobeWindow : Window, INotifyPropertyChanged
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


        public SobeWindow()
        {
            InitializeComponent();
            popuniTabelu();
            /*
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.sobe);
            view.SortDescriptions.Add(new SortDescription("Broj", ListSortDirection.Descending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("TipS"));
            //view.Filter = new Predicate<object>(MyFilter);


            dgSobe.ItemsSource = view;

            //dgSobe.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgSobe.AutoGenerateColumns = false;
            //dgSobe.IsReadOnly = true;
            */
            //btnDelete.IsEnabled = false;
            //btnEdit.IsEnabled = false;
            this.DataContext = this;
        }

        public bool MyFilter(object o)
        {
            Soba s = o as Soba;
            return s.Broj >= 2;
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            IzmeniSobuWindow isw = new IzmeniSobuWindow(this, IzmeniSobuWindow.STANJE.IZMENA);
            isw.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            IzmeniSobuWindow isw = new IzmeniSobuWindow(this, IzmeniSobuWindow.STANJE.DODAVANJE);
            isw.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni???", "Brisanje sobe", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Soba s = dgSobe.SelectedItem as Soba;
                Aplikacija.Instanca.hotel.sobe.Remove(s);
                SobaDAO.DeleteLogicko(s);
                dgSobe.Items.Refresh();
            }
        }

        private void dgSobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnEdit.IsEnabled = true;
            //btnDelete.IsEnabled = true;
            this.IsButtonEnabled = true;
        }

        private void IzlistajTipoveSoba_Click(object sender, RoutedEventArgs e)
        {
            TipoviSobeWindow tsw = new TipoviSobeWindow();
            tsw.Show();
            this.Hide();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string unetTekst = tbUneto.Text;
            if (unetTekst != "")
            {
                popuniTabelu();
                ObservableCollection<Soba> filtriranaLista = new ObservableCollection<Soba>();
                foreach (Soba s in dgSobe.Items)
                {
                    if (s.TipS.Naziv.Contains(unetTekst))
                    {
                        filtriranaLista.Add(s);
                    }
                }
                dgSobe.ItemsSource = filtriranaLista;
            }
            else popuniTabelu();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlavniMeniWindow glavniProzor = new GlavniMeniWindow();
            glavniProzor.Show();

        }

        private void popuniTabelu()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.sobe);
            view.SortDescriptions.Add(new SortDescription("Broj", ListSortDirection.Ascending));
            //view.GroupDescriptions.Add(new PropertyGroupDescription("TipS"));
            dgSobe.ItemsSource = view;

            dgSobe.AutoGenerateColumns = false;
        }

        private void popuniTabelu(ObservableCollection<Soba> lista)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lista);
            view.SortDescriptions.Add(new SortDescription("Broj", ListSortDirection.Ascending));
            //view.GroupDescriptions.Add(new PropertyGroupDescription("TipS"));
            dgSobe.ItemsSource = view;

            dgSobe.AutoGenerateColumns = false;
        }

        private void rbSlobodneSobe_Click(object sender, RoutedEventArgs e)
        {
            Aplikacija.Instanca.hotel.upisiZauzeteSobe();
            ObservableCollection<Soba> slobodneSobe = Aplikacija.Instanca.hotel.prikaziSlobodneSobe();
            popuniTabelu(slobodneSobe);
            //dgSobe.Items.Refresh();
        }

        private void rbZauzeteSobe_Click(object sender, RoutedEventArgs e)
        {
            Aplikacija.Instanca.hotel.upisiZauzeteSobe();
            ObservableCollection<Soba> zauzeteSobe = Aplikacija.Instanca.hotel.prikaziZauzeteSobe();
            popuniTabelu(zauzeteSobe);
        }
    }
}
