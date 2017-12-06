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
    /// Interaction logic for IzmeniKorisnikaWindow.xaml
    /// </summary>
    public partial class IzmeniKorisnikaWindow : Window
    {
        private KorisniciWindow korisniciWindow;
        private Korisnik korisnik, orgKorisnik;
        public enum STANJE { DODAVANJE, IZMENA }
        private STANJE trenutnoStanje;

        public IzmeniKorisnikaWindow()
        {
            InitializeComponent();
        }

        public IzmeniKorisnikaWindow(KorisniciWindow korisniciWindow, STANJE st)
            : this()
        {
            this.korisniciWindow = korisniciWindow;
            this.trenutnoStanje = st;
            if (st == STANJE.IZMENA)
            {
                this.orgKorisnik = korisniciWindow.dgKorisnici.SelectedItem as Korisnik;
                this.korisnik = orgKorisnik.Clone() as Korisnik;
            }
            else
            {
                this.korisnik = new Korisnik();
                this.orgKorisnik = korisnik;
            }

            this.DataContext = korisnik;

            foreach (var item in Korisnik.TIP_KORISNIKA.GetValues(typeof(Korisnik.TIP_KORISNIKA)))
            {
                cbTipKorisnika.Items.Add(item);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            korisniciWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            korisniciWindow.Show();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (proveraLozinke(korisnik))
            {
                orgKorisnik.FillData(korisnik);

                if (trenutnoStanje == STANJE.DODAVANJE)
                {
                    Aplikacija.korisnici.Add(korisnik);
                    KorisnikDAO.Create(korisnik);

                }
                else
                {
                    KorisnikDAO.Edit(korisnik);
                }

                korisniciWindow.Show();
                this.Close();
            }
        }

        private bool proveraLozinke(Korisnik k)
        {
            if (pbPassword.Password == pbPonoviPassword.Password)
            {
                k.Lozinka = pbPassword.Password;
                return true;

            }
            else if (pbPassword == null && pbPonoviPassword == null)
            {
                k.Lozinka = orgKorisnik.Lozinka;
                return true;
            }
            else
            {
                MessageBox.Show("Lozinke koje ste uneli se ne poklapaju, pokusajte ponovo", "Neispravne lozinke", MessageBoxButton.OK);
                return false;
            }
        }

    }
}
