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
    /// Interaction logic for UnosParametaraPosetaWindow.xaml
    /// </summary>
    public partial class UnosParametaraPosetaWindow : Window
    {
        public UnosParametaraPosetaWindow()
        {
            InitializeComponent();
        }

        private void btnPrikazi_Click(object sender, RoutedEventArgs e)
        {
            DateTime pocDat = datePickerPocetni.SelectedDate.Value;
            DateTime zavDat = datePickerZavrsni.SelectedDate.Value;
            IzvestajPosetaWindow ipw = new IzvestajPosetaWindow(pocDat, zavDat);
            ipw.Show();
            this.Close();
        }
    }
}
