using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class Korisnik : Osoba, ICloneable, INotifyPropertyChanged
    {
        private string korisnickoIme;
        private string lozinka;
        public enum TIP_KORISNIKA { Administrator, Recepcioner }
        private TIP_KORISNIKA tipKorisnika;
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /*    private string tipKorisnika;

            public string TipKorisnika
            {
                get { return tipKorisnika; }
                set { tipKorisnika = value;
                      OnPropertyChanged("TipKorisnika"); }
            }
          */

        public TIP_KORISNIKA TipKorisnika
        {
            get { return tipKorisnika; }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

        public string Lozinka
        {
            get { return lozinka; }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }


        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set
            {
                korisnickoIme = value;
                OnPropertyChanged("KorisnickoIme");
            }
        }


        public Korisnik() : base() { }

        public Korisnik(string ime, string prezime, string JMBG, string korisnickoIme, string lozinka, TIP_KORISNIKA tipKorisnika)
            : base(ime, prezime, JMBG)
        {
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.tipKorisnika = tipKorisnika;
        }

        public override sealed void pozdrav()
        {
            Console.WriteLine("Pozdrav iz Korisnika!!!!"); ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Korisnik copy = new Korisnik(this.Ime, this.Prezime, this.JMBG, this.KorisnickoIme, this.Lozinka, this.TipKorisnika);
            return copy;
        }

        public void FillData(Korisnik k)
        {
            this.Ime = k.Ime;
            this.Prezime = k.Prezime;
            this.JMBG = k.JMBG;
            this.KorisnickoIme = k.KorisnickoIme;
            this.Lozinka = k.Lozinka;
            this.TipKorisnika = k.TipKorisnika;
        }
    }
}
