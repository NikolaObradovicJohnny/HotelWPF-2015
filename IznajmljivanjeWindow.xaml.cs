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
    /// Interaction logic for IznajmljivanjeWindow.xaml
    /// </summary>
    public partial class IznajmljivanjeWindow : Window, INotifyPropertyChanged
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

        public IznajmljivanjeWindow()
        {
            InitializeComponent();
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.iznajmljivanja);
            view.SortDescriptions.Add(new SortDescription("PocetniDatum", ListSortDirection.Ascending));
            dgIznajmljivanja.ItemsSource = view;
            dgIznajmljivanja.IsReadOnly = true;
            //.........
            //Iznajmljivanje izn = dgIznajmljivanja.SelectedItem as Iznajmljivanje;
            //lvGosti.ItemsSource = izn.gosti;
            //.........

            lvGosti.DisplayMemberPath = "Prezime";
            if (dgIznajmljivanja.CurrentItem != null)
            {
                Iznajmljivanje selektovanoIznajmljivanje = dgIznajmljivanja.CurrentItem as Iznajmljivanje;
                CollectionViewSource cvs = new CollectionViewSource();
                cvs.Source = selektovanoIznajmljivanje.Gosti;
                lvGosti.ItemsSource = cvs.View;
                lvGosti.IsSynchronizedWithCurrentItem = true;
            }


            dgIznajmljivanja.AutoGenerateColumns = false;
            this.DataContext = this;

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            IzmeniIznajmljivanjeWindow ikw = new IzmeniIznajmljivanjeWindow(this, IzmeniIznajmljivanjeWindow.STANJE.DODAVANJE);
            ikw.Show();
            this.Hide();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            IzmeniIznajmljivanjeWindow ikw = new IzmeniIznajmljivanjeWindow(this, IzmeniIznajmljivanjeWindow.STANJE.IZMENA);
            ikw.Show();
            this.Hide();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni???", "Brisanje iznajmljivanja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Iznajmljivanje i = dgIznajmljivanja.SelectedItem as Iznajmljivanje;
                //i.Gosti = new ObservableCollection<Gost>();
                foreach (Gost gost in i.Gosti)
                {
                    //i.Gosti.Remove(gost);
                    GostDAO.DeleteLogicko(gost);
                }
                Aplikacija.Instanca.hotel.iznajmljivanja.Remove(i);
                IznajmljivanjeDAO.Delete(i);
                dgIznajmljivanja.Items.Refresh();
            }
        }

        private void dgIznajmljivanja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsButtonEnabled = true;
            Iznajmljivanje selektovani = dgIznajmljivanja.SelectedItem as Iznajmljivanje;
            if (selektovani != null)
            {
                lvGosti.ItemsSource = selektovani.Gosti;
            } else
            lvGosti.ItemsSource = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MeniRecepcionerWindow meniRecepcioner = new MeniRecepcionerWindow();
            meniRecepcioner.Show();
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MeniRecepcionerWindow meniRecepcioner = new MeniRecepcionerWindow();
            meniRecepcioner.Show();
            this.Close();

        }

        private void btnPretragaPoBrojuSobe_Click(object sender, RoutedEventArgs e)
        {
            string unetTekst = tbUnesiBroj.Text;
            if (unetTekst != "")
            {
                popuniTabelu();
                ObservableCollection<Iznajmljivanje> filtriranaLista = new ObservableCollection<Iznajmljivanje>();
                foreach (Iznajmljivanje i in dgIznajmljivanja.Items)
                {
                    if (i.Soba.Broj == Int32.Parse(unetTekst))
                    {
                        filtriranaLista.Add(i);
                    }
                }
                dgIznajmljivanja.ItemsSource = filtriranaLista;
            }
            else popuniTabelu();
        }

        private void popuniTabelu(ObservableCollection<Iznajmljivanje> lista)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lista);
            view.SortDescriptions.Add(new SortDescription("Broj", ListSortDirection.Ascending));
            //view.GroupDescriptions.Add(new PropertyGroupDescription("TipS"));
            dgIznajmljivanja.ItemsSource = view;
            dgIznajmljivanja.IsReadOnly = true;

            dgIznajmljivanja.AutoGenerateColumns = false;
        }

        private void popuniTabelu()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.iznajmljivanja);
            view.SortDescriptions.Add(new SortDescription("PocetniDatum", ListSortDirection.Ascending));
            dgIznajmljivanja.ItemsSource = view;
            dgIznajmljivanja.IsReadOnly = true;

            dgIznajmljivanja.AutoGenerateColumns = false;
        }

        private void btnPretragaPoDolasku_Click(object sender, RoutedEventArgs e)
        {
            DateTime unetDatum = datePickerPretragaPoDolasku.SelectedDate.Value;

            popuniTabelu();
            ObservableCollection<Iznajmljivanje> filtriranaLista = new ObservableCollection<Iznajmljivanje>();
            foreach (Iznajmljivanje i in dgIznajmljivanja.Items)
            {
                if (i.PocetniDatum == unetDatum)
                {
                    filtriranaLista.Add(i);
                }
            }
            dgIznajmljivanja.ItemsSource = filtriranaLista;

        }

        private void btnPretragaPoOdlasku_Click(object sender, RoutedEventArgs e)
        {
            DateTime unetDatum = datePickerPretragaPoOdlasku.SelectedDate.Value;

            popuniTabelu();
            ObservableCollection<Iznajmljivanje> filtriranaLista = new ObservableCollection<Iznajmljivanje>();
            foreach (Iznajmljivanje i in dgIznajmljivanja.Items)
            {
                if (i.ZavrsniDatum == unetDatum)
                {
                    filtriranaLista.Add(i);
                }
            }
            dgIznajmljivanja.ItemsSource = filtriranaLista;
        }

    }
}
