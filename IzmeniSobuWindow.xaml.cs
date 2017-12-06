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
    /// Interaction logic for IzmeniSobuWindow.xaml
    /// </summary>
    public partial class IzmeniSobuWindow : Window
    {
        private SobeWindow sobeWindow;
        private Soba soba, orgSoba;
        public enum STANJE { DODAVANJE, IZMENA }
        private STANJE trenutnoStanje;

        public IzmeniSobuWindow()
        {
            InitializeComponent();
        }

        public IzmeniSobuWindow(SobeWindow sobeWindow, STANJE st)
            : this()
        {
            this.sobeWindow = sobeWindow;
            this.trenutnoStanje = st;
            if (st == STANJE.IZMENA)
            {
                this.orgSoba = sobeWindow.dgSobe.SelectedItem as Soba;
                this.soba = orgSoba.Clone() as Soba;
                //tbBrojSobe.Text = soba.Broj.ToString();
                //combTipSobe.ItemsSource = Aplikacija.Instanca.hotel.tipoviSobe;
                //combTipSobe.SelectedItem = soba.tipSobe;
                //chbTv.IsChecked = soba.tv;
                //chbMiniBar.IsChecked = soba.miniBar;
            }
            else
            {
                this.soba = new Soba();
                this.orgSoba = soba;
            }

            this.DataContext = soba;
            combTipSobe.ItemsSource = Aplikacija.Instanca.hotel.tipoviSobe;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //   string brojsobestring = tbBrojSobe.Text;
            //   int brojSobe = Int32.Parse(brojsobestring);
            //   TipSobe tipSobe = combTipSobe.SelectedItem as TipSobe;
            //   bool tv = false;
            //   bool miniBar = false;
            //  if (chbTv.IsEnabled)
            //  {
            //       tv = true;
            //   }
            //   if (chbMiniBar.IsEnabled)
            //   {
            //       miniBar = true;
            //   }
            /* drugi nacin za dodavanje checkbox
             * 
             * soba.tv = chbTv.IsChecked.Value;
             * 
             * 
             * */

            //    soba.Broj = brojSobe;
            //    soba.tipSobe = tipSobe;
            //    soba.tv = tv;
            //    soba.miniBar = miniBar;

            orgSoba.FillData(soba);

            if (trenutnoStanje == STANJE.DODAVANJE)
            {
                SobaDAO.Create(orgSoba);
                Aplikacija.Instanca.hotel.sobe.Add(orgSoba);

            }
            else
            {
                SobaDAO.Update(orgSoba);
            }

            //sobeWindow.dgSobe.Items.Refresh();
            sobeWindow.Show();
            this.Close();


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            sobeWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sobeWindow.Show();

        }
    }
}
