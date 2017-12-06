using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class Osoba
    {
        private string ime;
        private string prezime;
        private string jmbg;

        public string JMBG
        {
            get { return jmbg; }
            set { jmbg = value; }
        }


        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }


        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }



        public virtual void pozdrav()
        {
            Console.WriteLine("Pozdrav od Osobe!");
        }

        public Osoba()
        {
            this.ime = "";
            this.prezime = "";
            this.jmbg = "";
        }

        public Osoba(string ime, string prezime, string jmbg)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
        }
    }
}
