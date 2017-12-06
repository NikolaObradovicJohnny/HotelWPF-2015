using NoviHotelWPF.DAOs;
using NoviHotelWPF.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for IzmeniIznajmljivanjeWindow.xaml
    /// </summary>
    public partial class IzmeniIznajmljivanjeWindow : Window
    {
        private IznajmljivanjeWindow iznajmljivanjeWindow;
        private Iznajmljivanje iznajmljivanje, orgIznajmljivanje;
        public enum STANJE { DODAVANJE, IZMENA }
        private STANJE trenutnoStanje;

        public IzmeniIznajmljivanjeWindow()
        {
            InitializeComponent();
            Aplikacija.Instanca.hotel.upisiZauzeteSobe();
            cbSoba.ItemsSource = Aplikacija.Instanca.hotel.prikaziSlobodneSobe();
            combTipIznajmljivanja.ItemsSource = Aplikacija.Instanca.hotel.tipoviIznajmljivanja;
        }

        public IzmeniIznajmljivanjeWindow(IznajmljivanjeWindow iznajmljivanjeWindow, STANJE st)
            : this()
        {
            this.iznajmljivanjeWindow = iznajmljivanjeWindow;
            this.trenutnoStanje = st;
            if (st == STANJE.IZMENA)
            {
                this.orgIznajmljivanje = iznajmljivanjeWindow.dgIznajmljivanja.SelectedItem as Iznajmljivanje;
                this.iznajmljivanje = orgIznajmljivanje.Clone() as Iznajmljivanje;
                
            }
            else
            {
                this.iznajmljivanje = new Iznajmljivanje();
                this.orgIznajmljivanje = iznajmljivanje;
            }

            this.DataContext = iznajmljivanje;
            dgGosti.ItemsSource = iznajmljivanje.Gosti;
            /*
            if (dgGosti.Items.Count == (cbSoba.SelectedItem as Soba).TipS.BrojKreveta)
            {
                btnDodajGosta.IsEnabled = false;
            }
            */
            //dgGosti.AutoGenerateColumns = false;
            //dgGosti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            //datePickerPocetni.DisplayDateStart = DateTime.Today;
            //datePickerPocetni.SelectedDate = DateTime.Today;
            //datePickerPocetni.Text.StartsWith("1.4.2016.");
            //datePickerPocetni.Value = DateTime.Today;
            //datePickerZavrsni.DisplayDateStart = DateTime.Today;
            /*if (iznajmljivanje == null)
            {
                datePickerPocetni.DisplayDate = "1/1/2016";
                datePickerZavrsni.DisplayDate = DateTime.Today as String;
            }
            */
        }

        private void btnDodajGosta_Click(object sender, RoutedEventArgs e)
        {
            DodajGostaWindow dgw = new DodajGostaWindow();
            if (dgw.ShowDialog() == true)
            {
                //this.orgIznajmljivanje.Gosti.Add(dgw.Gost);


                dgGosti.Items.Add(dgw.Gost);
                

            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            orgIznajmljivanje.FillData(iznajmljivanje);

            if (trenutnoStanje == STANJE.DODAVANJE)
            {
                IznajmljivanjeDAO.Create(orgIznajmljivanje);
                foreach (Gost g in dgGosti.Items)
                {
                    GostDAO.Create(g, (long)Aplikacija.Instanca.hotel.posete_bezObziraDaLiPostojeIliNe.Count());
                    //orgIznajmljivanje.Gosti.Add(g);
                }
                Aplikacija.Instanca.hotel.iznajmljivanja.Add(orgIznajmljivanje);

            }
            else
            {
                IznajmljivanjeDAO.Update(orgIznajmljivanje);
                foreach (Gost g in dgGosti.Items)
                {
                    GostDAO.Edit(g);
                }
            }

            iznajmljivanjeWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            iznajmljivanjeWindow.Show();

        }

        private void btnIzmeniGosta_Click(object sender, RoutedEventArgs e)
        {
            DodajGostaWindow dgw = new DodajGostaWindow(this, DodajGostaWindow.STANJE.IZMENA);
            if (dgw.ShowDialog() == true)
            {
                //this.orgIznajmljivanje.Gosti.Add(dgw.Gost);
                dgGosti.Items.Refresh();

                //dgGosti.Items.Add(dgw.Gost);

            }
        }

        private void btnIzbrisiGosta_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni???", "Brisanje gosta", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Gost g = dgGosti.SelectedItem as Gost;
                //i.Gosti = new ObservableCollection<Gost>();
                //GostDAO.DeleteLogicko(g);
                //this.iznajmljivanje.Gosti.Remove(g);
                //GostDAO.DeleteLogicko(g);
                dgGosti.Items.Remove(g);
                if (Aplikacija.Instanca.hotel.da_li_postoji_gost(g) == true)
                {
                    GostDAO.DeleteLogicko(g);
                }
                dgGosti.Items.Refresh();
            }
        }

        private void dgGosti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgGosti.Items.Count == (cbSoba.SelectedItem as Soba).TipS.BrojKreveta)
            {
                btnDodajGosta.IsEnabled = false;
            }
            else btnDodajGosta.IsEnabled = true;
        }

    }
}
