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
    /// Interaction logic for IzmeniCenovnikWindow.xaml
    /// </summary>
    public partial class IzmeniCenovnikWindow : Window
    {
        private CenovnikWindow cenovnikWindow;
        private Cenovnik cenovnik, orgCenovnik;
        public enum STANJE { DODAVANJE, IZMENA }
        private STANJE trenutnoStanje;

        public IzmeniCenovnikWindow()
        {
            InitializeComponent();
            //cbTipSobe.ItemsSource = Aplikacija.Instanca.hotel.prikaziSlobodneTipoveSobe_za_cenovnik();
            cbTipSobe.ItemsSource = Aplikacija.Instanca.hotel.tipoviSobe;
            cbTipIznajmljivanja.ItemsSource = Aplikacija.Instanca.hotel.tipoviIznajmljivanja;
        }

        public IzmeniCenovnikWindow(CenovnikWindow cenovnikWindow, STANJE st)
            : this()
        {
            this.cenovnikWindow = cenovnikWindow;
            this.trenutnoStanje = st;
            if (st == STANJE.IZMENA)
            {
                this.orgCenovnik = cenovnikWindow.dgCenovnik.SelectedItem as Cenovnik;
                this.cenovnik = orgCenovnik.Clone() as Cenovnik;
            }
            else
            {
                this.cenovnik = new Cenovnik();
                this.orgCenovnik = cenovnik;
            }

            this.DataContext = cenovnik;

        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (proveraPopunjenihPolja())
            {
                orgCenovnik.FillData(cenovnik);

                if (trenutnoStanje == STANJE.DODAVANJE)
                {
                    CenovnikDAO.Create(orgCenovnik);
                    Aplikacija.Instanca.hotel.cenovnik.Add(orgCenovnik);

                }
                else
                {
                    CenovnikDAO.Edit(orgCenovnik);
                }

                cenovnikWindow.Show();
                this.Close();
            }/*

            orgCenovnik.FillData(cenovnik);

            if (trenutnoStanje == STANJE.DODAVANJE)
            {
                CenovnikDAO.Create(orgCenovnik);
                Aplikacija.Instanca.hotel.cenovnik.Add(orgCenovnik);

            }
            else
            {
                CenovnikDAO.Edit(orgCenovnik);
            }

            cenovnikWindow.Show();
            this.Close();
              */
        }

        private bool proveraPopunjenihPolja()
        {
            if (cbTipIznajmljivanja.SelectedItem == null || cbTipSobe.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli sve podatke", "Prazna polja", MessageBoxButton.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cenovnikWindow.Show();

        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
