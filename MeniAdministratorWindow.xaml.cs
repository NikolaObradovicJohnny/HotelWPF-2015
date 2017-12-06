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
    /// Interaction logic for MeniAdministratorWindow.xaml
    /// </summary>
    public partial class MeniAdministratorWindow : Window
    {
        public MeniAdministratorWindow()
        {
            InitializeComponent();
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

        private void PrikazCenovnik_Click(object sender, RoutedEventArgs e)
        {
            CenovnikWindow cw = new CenovnikWindow();
            cw.ShowDialog();
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

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logovanje = new LogInWindow();
            Aplikacija.Instanca.Ulogovan = null;
            logovanje.Show();
            this.Close();
        }

    }
}
