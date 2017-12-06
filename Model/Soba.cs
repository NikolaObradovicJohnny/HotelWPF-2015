using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class Soba : ICloneable, INotifyPropertyChanged
    {
        //private int broj;
        //public TipSobe tipSobe { get; set; }
        //public bool tv { get; set; }
        //public bool miniBar { get; set; }

        private int broj;
        private TipSobe tipS;
        private bool tv;
        private bool miniBar;
        private long id;
        private bool jeZauzeta;

        public bool JeZauzeta
        {
            get { return jeZauzeta; }
            set { jeZauzeta = value; }
        }


        public long Id
        {
            get { return id; }
            set { id = value; }
        }


        public bool MiniBar
        {
            get { return miniBar; }
            set
            {
                miniBar = value;
                OnPropertyChanged("MiniBar");
            }
        }

        public bool Tv
        {
            get { return tv; }
            set
            {
                tv = value;
                OnPropertyChanged("Tv");
            }
        }

        public TipSobe TipS
        {
            get { return tipS; }
            set
            {
                tipS = value;
                OnPropertyChanged("TipS");
            }
        }


        public int Broj
        {
            get { return broj; }
            set
            {
                broj = value;
                OnPropertyChanged("Broj");
            }
        }


        public Soba() { }

        public Soba(int broj, TipSobe tipSobe, bool tv, bool miniBar)
        {
            this.broj = broj;
            this.tipS = tipSobe;
            this.tv = tv;
            this.miniBar = miniBar;

        }

        public override string ToString()
        {
            return "Soba broj: " + this.broj + "| " + this.tipS.ToString();
        }



        public object Clone()
        {
            Soba copy = new Soba(this.Broj, this.tipS, this.tv, this.miniBar);
            return copy;
        }

        public void FillData(Soba s)
        {
            this.Broj = s.Broj;
            this.TipS = s.TipS;
            this.Tv = s.Tv;
            this.MiniBar = s.MiniBar;
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
