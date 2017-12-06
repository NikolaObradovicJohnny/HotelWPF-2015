using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class Gost : Osoba
    {
        private long id;
        private string brojLicneKarte;
        private long idIznajmljivanja;

        public long IdIznajmljivanja
        {
            get { return idIznajmljivanja; }
            set { idIznajmljivanja = value; }
        }


        public string BrojLicneKarte
        {
            get { return brojLicneKarte; }
            set { brojLicneKarte = value; }
        }


        public long Id
        {
            get { return id; }
            set { id = value; }
        }


        //public string brojLicneKarte { get; set; }

        public Gost() : base() { }

        public Gost(string ime, string prezime, string JMBG, string brojLicneKarte)
            : base(ime, prezime, JMBG)
        {
            this.brojLicneKarte = brojLicneKarte;
        }

        public override string ToString()
        {
            return "Gost: " + this.Ime + " " + this.Prezime + "|JMBG:" + this.JMBG + "|broj licne karte:" + this.brojLicneKarte;
        }
    }
}
