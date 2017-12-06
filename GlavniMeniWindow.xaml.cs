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
    /// Interaction logic for GlavniMeniWindow.xaml
    /// </summary>
    public partial class GlavniMeniWindow : Window
    {
        
        private Korisnik ulogovan;

        public Korisnik Ulogovan
        {
            get { return ulogovan; }
            set { ulogovan = value; }
        }


        public GlavniMeniWindow()
        {
            InitializeComponent();
            //miKorisnici.Visibility = Visibility.Hidden;
        }

        public GlavniMeniWindow(Korisnik ulogovan)
            : base()
        {
            if (ulogovan.TipKorisnika == Korisnik.TIP_KORISNIKA.Administrator)
            {
                PrikaziIznajmljivanja.IsEnabled = false;
            }
            else if (ulogovan.TipKorisnika == Korisnik.TIP_KORISNIKA.Recepcioner)
            {
                PrikazCenovnik.IsEnabled = false;
                PrikaziKorisnike.IsEnabled = false;
                PrikaziSobe.IsEnabled = false;
            }
        }

        private void PrikaziKorisnike_Click(object sender, RoutedEventArgs e)
        {
            KorisniciWindow kw = new KorisniciWindow();
            kw.Show();
            this.Hide();
        }

        private void PrikaziSobe_Click(object sender, RoutedEventArgs e)
        {
            SobeWindow sw = new SobeWindow();
            sw.ShowDialog();
            //this.Hide();
        }

        private void PrikaziIznajmljivanja_Click(object sender, RoutedEventArgs e)
        {
            IznajmljivanjeWindow iw = new IznajmljivanjeWindow();
            iw.Show();
            this.Hide();
        }

        private void PrikazCenovnik_Click(object sender, RoutedEventArgs e)
        {
            CenovnikWindow cw = new CenovnikWindow();
            cw.ShowDialog();
            //this.Hide();
        }

        private void PrikaziGoste_Click(object sender, RoutedEventArgs e)
        {
            GostiWindow gw = new GostiWindow();
            gw.ShowDialog();
            //this.Hide();
        }

        private void PrikaziTipoveSoba_Click(object sender, RoutedEventArgs e)
        {
            TipoviSobeWindow tsw = new TipoviSobeWindow();
            tsw.Show();
            this.Hide();
        }

        private void PrikazIzvestaja_Sobe_Click(object sender, RoutedEventArgs e)
        {
            IzvestajSobeWindow isw = new IzvestajSobeWindow();
            isw.Show();
        }

        private void PrikazIzvestaja_Poseta_Click(object sender, RoutedEventArgs e)
        {
            //IzvestajPosetaWindow ipw = new IzvestajPosetaWindow();
            UnosParametaraPosetaWindow uppw = new UnosParametaraPosetaWindow();
            uppw.Show();
        }

        private void PrikazIzvestaja_Test_Click(object sender, RoutedEventArgs e)
        {
            IzvestajSobeWindow isw = new IzvestajSobeWindow();
            isw.Show();
        }

        private void PrikaziAktuelnihIznajmljivanja_Click(object sender, RoutedEventArgs e)
        {
            AktuelnaIznajmljivanjaWindow aiw = new AktuelnaIznajmljivanjaWindow();
            aiw.ShowDialog();
        }

        //private void PrikazIzvestaja_Sobe_Click(object sender, RoutedEventArgs e)
        //{
        //    //IzvestajWindow iw = new IzvestajWindow();
        //    iw.Show();
        //}

        //private void PrikazIzvestaja_Poseta_Click(object sender, RoutedEventArgs e)
        //{
        //    UnosIzvestajPosetaWindow uipw = new UnosIzvestajPosetaWindow();
        //    uipw.Show();
        //}
    }
}
