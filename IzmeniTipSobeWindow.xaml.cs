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
    /// Interaction logic for IzmeniTipSobeWindow.xaml
    /// </summary>
    public partial class IzmeniTipSobeWindow : Window
    {
        private TipoviSobeWindow tipoviSobeWindow;
        private TipSobe tipSobe, orgTipSobe;
        public enum STANJE { DODAVANJE, IZMENA }
        private STANJE trenutnoStanje;

        public IzmeniTipSobeWindow()
        {
            InitializeComponent();
        }

        public IzmeniTipSobeWindow(TipoviSobeWindow tipoviSobeWindow, STANJE st)
            : this()
        {
            this.tipoviSobeWindow = tipoviSobeWindow;
            this.trenutnoStanje = st;
            if (st == STANJE.IZMENA)
            {
                this.orgTipSobe = tipoviSobeWindow.dgTipoviSobe.SelectedItem as TipSobe;
                this.tipSobe = orgTipSobe.Clone() as TipSobe;
            }
            else
            {
                this.tipSobe = new TipSobe();
                this.orgTipSobe = tipSobe;
            }

            this.DataContext = tipSobe;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            orgTipSobe.FillData(tipSobe);

            if (trenutnoStanje == STANJE.DODAVANJE)
            {
                TipSobeDAO.Create(orgTipSobe);
                Aplikacija.Instanca.hotel.tipoviSobe.Add(orgTipSobe);

            }
            else
            {
                TipSobeDAO.Update(orgTipSobe);
            }

            tipoviSobeWindow.Show();
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            tipoviSobeWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tipoviSobeWindow.Show();
        }
    }
}
