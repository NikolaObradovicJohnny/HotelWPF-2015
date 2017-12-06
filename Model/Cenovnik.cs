using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class Cenovnik : ICloneable, INotifyPropertyChanged
    {
        //public TipSobe tipSobe { get; set; }
        //public TipIznajmljivanja tipIznajmljivanja { get; set; }
        private decimal cena;
        private TipSobe tipS;
        private TipIznajmljivanja tipIznajmljivanja;

        public TipIznajmljivanja TipIznajmljivanja
        {
            get { return tipIznajmljivanja; }
            set { tipIznajmljivanja = value; }
        }


        public TipSobe TipS
        {
            get { return tipS; }
            set { tipS = value; }
        }

        public decimal Cena
        {
            get { return cena; }
            set { cena = value; }
        }

        public Cenovnik() { }

        public Cenovnik(TipSobe tipSobe, TipIznajmljivanja tipIznajmljivanja, decimal cena)
        {
            this.TipS = tipSobe;
            this.TipIznajmljivanja = tipIznajmljivanja;
            this.Cena = cena;
        }

        /*

        public static double izracunajCenu(TipIznajmljivanja tipIznajmljivanja, TipSobe tipSobe)
        {
            double ukupnaCena = 0;
            double brojSaKojimMnozim = 20;
            ukupnaCena = ukupnaCena + brojSaKojimMnozim * tipSobe.brojKreveta;
            if (tipIznajmljivanja == TipIznajmljivanja.DnevniBoravak)
            {
                ukupnaCena = ukupnaCena * 2;
                return ukupnaCena;

            }
            else if (tipIznajmljivanja == TipIznajmljivanja.Nocenje)
            {
                ukupnaCena = ukupnaCena * 3;
                return ukupnaCena;
            }
        }                                 */

        public override string ToString()
        {
            return "Cenovnik: " + this.tipS + "| " + this.tipIznajmljivanja + "| cena:" + this.Cena;
        }

        public object Clone()
        {
            Cenovnik copy = new Cenovnik(TipS, TipIznajmljivanja, Cena);
            return copy;
        }

        public void FillData(Cenovnik c)
        {
            this.TipIznajmljivanja = c.TipIznajmljivanja;
            this.TipS = c.TipS;
            this.Cena = c.Cena;
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
