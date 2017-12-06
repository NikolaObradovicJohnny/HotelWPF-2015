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
    /// Interaction logic for CenovnikWindow.xaml
    /// </summary>
    public partial class CenovnikWindow : Window, INotifyPropertyChanged
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

        public CenovnikWindow()
        {
            InitializeComponent();

            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.cenovnik);
            view.SortDescriptions.Add(new SortDescription("TipS.Naziv", ListSortDirection.Descending));
            //view.GroupDescriptions.Add(new PropertyGroupDescription("TipS.Naziv"));

            //dgCenovnik.ItemsSource = Aplikacija.Instanca.hotel.cenovnik;
            //dgCenovnik.AutoGenerateColumns = false;

            dgCenovnik.ItemsSource = view;

            dgCenovnik.AutoGenerateColumns = false;
            this.DataContext = this;


        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("************");
            IzmeniCenovnikWindow dodajCenovnikWind = new IzmeniCenovnikWindow(this, IzmeniCenovnikWindow.STANJE.DODAVANJE);
            dodajCenovnikWind.Show();
            this.Hide();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            //IzmeniCenovnikWindow icw = new IzmeniCenovnikWindow(this, IzmeniCenovnikWindow.STANJE.IZMENA);
            IzmeniCenovnikWindow izmeniCenovnikWind = new IzmeniCenovnikWindow(this, IzmeniCenovnikWindow.STANJE.IZMENA);
            izmeniCenovnikWind.Show();
            this.Hide();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgCenovnik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsButtonEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlavniMeniWindow glavniProzor = new GlavniMeniWindow();
            glavniProzor.Show();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
