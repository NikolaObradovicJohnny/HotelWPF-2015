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
    /// Interaction logic for IzvestajSobeWindow.xaml
    /// </summary>
    public partial class IzvestajSobeWindow : Window
    {
        public IzvestajSobeWindow()
        {
            InitializeComponent();

            ReportDocument report = new ReportDocument();
            report.Load("../../IzvestajSobaCrystalReport.rpt");
            report.SetParameterValue("postoji", true);
            sobeViewer.ViewerCore.ReportSource = report;
        }

        
    }
}
