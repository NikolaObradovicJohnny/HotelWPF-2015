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
using NoviHotelWPF.Model;

namespace NoviHotelWPF
{
    /// <summary>
    /// Interaction logic for MeniRecepcionerWindow.xaml
    /// </summary>
    public partial class MeniRecepcionerWindow : Window
    {
        public MeniRecepcionerWindow()
        {
            InitializeComponent();
        }

        private void PrikaziIznajmljivanja_Click(object sender, RoutedEventArgs e)
        {
            IznajmljivanjeWindow iw = new IznajmljivanjeWindow();
            iw.Show();
            this.Hide();
        }

        private void PrikaziGoste_Click(object sender, RoutedEventArgs e)
        {
            GostiWindow gw = new GostiWindow();
            gw.ShowDialog();
            //this.Hide();
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

        private void PrikaziAktuelnihIznajmljivanja_Click(object sender, RoutedEventArgs e)
        {
            AktuelnaIznajmljivanjaWindow aiw = new AktuelnaIznajmljivanjaWindow();
            aiw.ShowDialog();
        }

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logovanje = new LogInWindow();
            Aplikacija.Instanca.Ulogovan = null;
            logovanje.Show();
            this.Close();
        }
    }
}
