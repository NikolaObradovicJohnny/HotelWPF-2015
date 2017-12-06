using NoviHotelWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for GostiWindow.xaml
    /// </summary>
    public partial class GostiWindow : Window
    {
        public GostiWindow()
        {
            InitializeComponent();
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.prikaziSveGoste());
            view.SortDescriptions.Add(new SortDescription("Prezime", ListSortDirection.Descending));

            dgGosti.ItemsSource = view;
            dgGosti.AutoGenerateColumns = false;
            this.DataContext = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlavniMeniWindow glavniProzor = new GlavniMeniWindow();
            glavniProzor.Show();

        }

    }
}
