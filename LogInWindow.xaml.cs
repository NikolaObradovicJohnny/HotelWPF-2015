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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = tbKIme.Text;
            string lozinka = pbKLozinka.Password;
            bool ulogovan = Aplikacija.Instanca.login(korisnickoIme, lozinka);
            if (ulogovan == true)
            {
                //GlavniMeniWindow glavniProzor = new GlavniMeniWindow();
                //glavniProzor.Show();

                Aplikacija.Instanca.Ulogovan = Aplikacija.Instanca.pronadjiUlogovanog(korisnickoIme, lozinka);
                if (Aplikacija.Instanca.Ulogovan.TipKorisnika == Model.Korisnik.TIP_KORISNIKA.Administrator)
                {
                    MeniAdministratorWindow meniAdmin = new MeniAdministratorWindow();
                    meniAdmin.Show();
                }
                else if (Aplikacija.Instanca.Ulogovan.TipKorisnika == Model.Korisnik.TIP_KORISNIKA.Recepcioner)
                {
                    MeniRecepcionerWindow meniRecepcioner = new MeniRecepcionerWindow();
                    meniRecepcioner.Show();
                }
                
                this.Close();

            }
            else
            {
                MessageBox.Show("Uneli ste pogresno korisnicko ime i/ili lozinku, pokusajte ponovo", "Neispravni podaci", MessageBoxButton.OK);
            }

        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
