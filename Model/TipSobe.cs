using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class TipSobe : ICloneable, INotifyPropertyChanged
    {
        //public string naziv { get; set; }
        //public int brojKreveta { get; set; }
        private long id;
        private int brojKreveta;
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }


        public int BrojKreveta
        {
            get { return brojKreveta; }
            set { brojKreveta = value; }
        }


        public long Id
        {
            get { return id; }
            set { id = value; }
        }


        public TipSobe() { }

        public TipSobe(string naziv, int brojKreveta)
        {
            this.naziv = naziv;
            this.brojKreveta = brojKreveta;
        }

        public override string ToString()
        {
            return this.naziv + " br.kreveta: " + this.brojKreveta;
        }

        public object Clone()
        {
            TipSobe copy = new TipSobe(Naziv, BrojKreveta);
            return copy;
        }

        public void FillData(TipSobe ts)
        {
            this.Naziv = ts.Naziv;
            this.BrojKreveta = ts.BrojKreveta;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
