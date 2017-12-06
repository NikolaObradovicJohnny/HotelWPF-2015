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
    /// Interaction logic for AktuelnaIznajmljivanjaWindow.xaml
    /// </summary>
    public partial class AktuelnaIznajmljivanjaWindow : Window
    {
        public AktuelnaIznajmljivanjaWindow()
        {
            InitializeComponent();
            ICollectionView view = CollectionViewSource.GetDefaultView(Aplikacija.Instanca.hotel.PrikaziAktuelnaIznajmljivanja());
            view.SortDescriptions.Add(new SortDescription("PocetniDatum", ListSortDirection.Ascending));
            dgAktuelnaIznajmljivanja.ItemsSource = view;
            dgAktuelnaIznajmljivanja.IsReadOnly = true;
            //.........
            //Iznajmljivanje izn = dgIznajmljivanja.SelectedItem as Iznajmljivanje;
            //lvGosti.ItemsSource = izn.gosti;
            //.........

            lvGosti.DisplayMemberPath = "Prezime";
            if (dgAktuelnaIznajmljivanja.CurrentItem != null)
            {
                Iznajmljivanje selektovanoIznajmljivanje = dgAktuelnaIznajmljivanja.CurrentItem as Iznajmljivanje;
                CollectionViewSource cvs = new CollectionViewSource();
                cvs.Source = selektovanoIznajmljivanje.Gosti;
                lvGosti.ItemsSource = cvs.View;
                lvGosti.IsSynchronizedWithCurrentItem = true;
            }


            dgAktuelnaIznajmljivanja.AutoGenerateColumns = false;
            this.DataContext = this;
        }

        private void dgAktuelnaIznajmljivanja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.IsButtonEnabled = true;
            Iznajmljivanje selektovani = dgAktuelnaIznajmljivanja.SelectedItem as Iznajmljivanje;
            lvGosti.ItemsSource = selektovani.Gosti;
        }

    }
}
