using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class Iznajmljivanje : ICloneable, INotifyPropertyChanged
    {
        //  public int idKod { get; set;}
        // public TipIznajmljivanja tipIznajmljivanja { get; set; }
        // public enum IzabraniTip TIP { DN_BORAVAK, NOCENJE};
        //public ObservableCollection<Gost> gosti;
        //public Soba soba { get; set;}
        //public DateTime pocetniDatum { get; set; }
        //public DateTime zavrsniDatum { get; set; }
        //public double ukupnaCena { get; set; }
        //public enum IzabraniTip { DnevniBoravak, Nocenje }
        //private IzabraniTip tipIznajmljivanja;
        //public IzabraniTip TipIznajmljivanja
        private long id;
        private ObservableCollection<Gost> gosti;

        public ObservableCollection<Gost> Gosti
        {
            get { return gosti; }
            set
            {
                gosti = value;
                OnPropertyChanged("Gosti");
            }
        }


        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private TipIznajmljivanja tipIznajmljivanja;
        private Soba soba;

        public Soba Soba
        {
            get { return soba; }
            set
            {
                soba = value;
                OnPropertyChanged("TipIznajmljivanja");
            }
        }


        public TipIznajmljivanja TipIznajmljivanja
        {
            get { return tipIznajmljivanja; }
            set
            {
                tipIznajmljivanja = value;
                OnPropertyChanged("TipIznajmljivanja");
            }
        }

        private DateTime pocDatum;
        private DateTime zavDatum;
        private decimal ukupnaCena;

        public decimal UkupnaCena
        {
            get { return ukupnaCena; }
            set { ukupnaCena = value; }
        }


        public DateTime ZavrsniDatum
        {
            get { return zavDatum; }
            set
            {
                zavDatum = value;
                OnPropertyChanged("ZavrsniDatum");
            }
        }


        public DateTime PocetniDatum
        {
            get { return pocDatum; }
            set
            {
                pocDatum = value;
                OnPropertyChanged("PocetniDatum");
                Console.WriteLine(value);
            }
        }



        public Iznajmljivanje()
        {
            ObservableCollection<Gost> gosti = new ObservableCollection<Gost>();
        }

        public Iznajmljivanje(TipIznajmljivanja tipIznajmljivanja, Soba soba, DateTime pocetniDatum, DateTime krajnjiDatum)
        {
            this.tipIznajmljivanja = tipIznajmljivanja;
            this.soba = soba;
            this.pocDatum = pocetniDatum;
            this.zavDatum = krajnjiDatum;
            this.ukupnaCena = this.IzracunajUkupnuCenu();
            this.gosti = new ObservableCollection<Gost>();

        }

        public Iznajmljivanje(TipIznajmljivanja tipIznajmljivanja, Soba soba, DateTime pocetniDatum, DateTime krajnjiDatum, ObservableCollection<Gost> gosti)
        {
            this.tipIznajmljivanja = tipIznajmljivanja;
            this.soba = soba;
            this.pocDatum = pocetniDatum;
            this.zavDatum = krajnjiDatum;
            this.ukupnaCena = this.IzracunajUkupnuCenu();
            this.gosti = gosti;

        }

        public override string ToString()
        {
            return "Iznajmljivanje: " + "id.kod:" + this.id + "|tip:" + this.tipIznajmljivanja + "|broj sobe:" + this.soba.Broj + "|pocetni datum:" + this.pocDatum.ToString("dd/MM/yyyy") + "|krajnji datum:" + this.zavDatum.ToString("dd/MM/yyyy") + "|ukupna cena:" + this.UkupnaCena + "/n";
        }

        public object Clone()
        {
            Iznajmljivanje copy = new Iznajmljivanje(TipIznajmljivanja, soba, pocDatum, zavDatum, gosti);
            //copy.gosti = new ObservableCollection<Gost>(gosti);
            return copy;
        }

        public void FillData(Iznajmljivanje i)
        {
            this.TipIznajmljivanja = i.TipIznajmljivanja;
            this.gosti = i.gosti;
            this.soba = i.soba;
            this.pocDatum = i.pocDatum;
            this.zavDatum = i.zavDatum;
            this.ukupnaCena = i.ukupnaCena;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public decimal IzracunajUkupnuCenu()
        {
            decimal cena = Aplikacija.Instanca.hotel.PronadjiCenu(this.TipIznajmljivanja, this.Soba);

            this.UkupnaCena = (decimal)(this.ZavrsniDatum - this.PocetniDatum).Days * cena;
            return Math.Round(this.UkupnaCena, 2);
        }

    }
}
