using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    class Aplikacija
    {
        public const string CONNECTION_STRING = @"data source=.\SQLEXPRESS; initial catalog=Hotel;integrated security=true;";
        private static Aplikacija instanca = new Aplikacija();
        public Hotel hotel;
        public static ObservableCollection<Korisnik> korisnici;
        private Korisnik ulogovan;

        public Korisnik Ulogovan
        {
            get { return ulogovan; }
            set { ulogovan = value; }
        }


        public static Aplikacija Instanca
        {
            get { return instanca; }
        }

        private Aplikacija()
        {
            hotel = new Hotel();
            korisnici = new ObservableCollection<Korisnik>();
            Ulogovan = new Korisnik();
            //dodajTestKorisnike();
            //KorisnikDAO.Load();
        }

        private void dodajTestKorisnike()
        {
            //hotel.LoadTestData();
            //hotel.LoadData();
            korisnici.Add(new Korisnik("Imenko", "Prezimenic", "123123123", "admin", "admin", Korisnik.TIP_KORISNIKA.Administrator));
            korisnici.Add(new Korisnik("Pera", "Peric", "123124443", "amin", "amin", Korisnik.TIP_KORISNIKA.Recepcioner));
            korisnici.Add(new Korisnik("Djuro", "Djurovic", "123333123", "dmin", "dmin", Korisnik.TIP_KORISNIKA.Recepcioner));
        }

        public bool login(string kIme, string kLoz)
        {
            //1.nacin
            foreach (Korisnik korisnik in korisnici)
            {
                if (korisnik.KorisnickoIme == kIme && korisnik.Lozinka == kLoz)
                {
                    Ulogovan = korisnik;
                    return true;
                }

            }
            return false;

            //2.nacin
            //var log = from k in korisnici where kIme == k.korisnickoIme && kLoz == k.lozinka select k;
            //if (log.Count() != 0)
            //{
            //    return true;
            //}
        }

        public Korisnik pronadjiKorisnikaPo_korisnickomImenu(string uneto)
        {
            foreach (Korisnik k in korisnici)
            {
                if (k.KorisnickoIme == uneto)
                {
                    return k;
                }
            }
            return null;
        }

        public Korisnik pronadjiUlogovanog(string kIme, string kLozinka)
        {
            foreach (Korisnik k in korisnici)
            {
                if (k.KorisnickoIme == kIme & k.Lozinka == kLozinka)
                {
                    return k;
                }
            }
            return null;
        }
    }
}
