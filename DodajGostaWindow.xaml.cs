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
    /// Interaction logic for DodajGostaWindow.xaml
    /// </summary>
    public partial class DodajGostaWindow : Window
    {
        public Gost Gost { get; set; }
        //private IzmeniIznajmljivanjeWindow izmenaProzor;
        public enum STANJE { DODAVANJE, IZMENA }
        //private STANJE trenutnoStanje;

        public DodajGostaWindow()
        {
            InitializeComponent();
            Gost = new Gost();

            this.DataContext = Gost;
        }

        public DodajGostaWindow(IzmeniIznajmljivanjeWindow izmenaProzor,STANJE st)
        {
            InitializeComponent();
            if (st == STANJE.DODAVANJE)
            {
                Gost = new Gost();
            }
            else if (st == STANJE.IZMENA)
            {
                Gost = izmenaProzor.dgGosti.SelectedItem as Gost;
            }

            //Gost = new Gost();

            this.DataContext = Gost;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            string ime = tbIme.Text;
            string prezime = tbPrezime.Text;
            string jmbg = tbJMBG.Text;
            string brLicne = tbLicne.Text;

            Gost.Ime = ime;
            Gost.Prezime = prezime;
            Gost.JMBG = jmbg;
            Gost.BrojLicneKarte = brLicne;

            //GostDAO.Create(Gost);

            this.Close();
        }
    }
}
