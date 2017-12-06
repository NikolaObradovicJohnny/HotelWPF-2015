using CrystalDecisions.CrystalReports.Engine;
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
    /// Interaction logic for IzvestajPosetaWindow.xaml
    /// </summary>
    public partial class IzvestajPosetaWindow : Window
    {
        public IzvestajPosetaWindow(DateTime datumPoc, DateTime datumZav)
        {
            InitializeComponent();

            ReportDocument report = new ReportDocument();
            report.Load("../../IzvestajPosetaCrystalReport.rpt");
            report.SetParameterValue("datumPocetka",datumPoc);
            report.SetParameterValue("datumZavrsetka", datumZav);
            report.SetParameterValue("postoji", true);
            poseteViewer.ViewerCore.ReportSource = report;
        }
    }
}
