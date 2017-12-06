using NoviHotelWPF.DAOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    class Hotel
    {
        public string naziv { get; set; }
        public double PIB { get; set; }
        public string adresa { get; set; }
        public string vlasnik { get; set; }
        public ObservableCollection<Soba> sobe;
        public ObservableCollection<Korisnik> korisnici;
        public ObservableCollection<Iznajmljivanje> iznajmljivanja;
        public ObservableCollection<TipSobe> tipoviSobe;
        public ObservableCollection<TipIznajmljivanja> tipoviIznajmljivanja;
        public ObservableCollection<Cenovnik> cenovnik;
        public ObservableCollection<Iznajmljivanje> posete_bezObziraDaLiPostojeIliNe;


        public Hotel()
        {
            sobe = new ObservableCollection<Soba>();

            tipoviSobe = new ObservableCollection<TipSobe>();

            tipoviIznajmljivanja = new ObservableCollection<TipIznajmljivanja>();

            cenovnik = new ObservableCollection<Cenovnik>();

            //ucitajSobeIzFajla();

            //korisnici = new ObservableCollection<Korisnik>();

            //ucitajKorisnikeIzFajla();

            iznajmljivanja = new ObservableCollection<Iznajmljivanje>();

            posete_bezObziraDaLiPostojeIliNe = new ObservableCollection<Iznajmljivanje>();

        }

        public void LoadTestData()
        {
            TipSobe tipsobe = new TipSobe("dvokrevetna", 2);
            tipoviSobe.Add(tipsobe);
            TipSobe tipsobe2 = new TipSobe("apartman", 5);
            tipoviSobe.Add(tipsobe2);
            sobe.Add(new Soba(1, tipsobe, true, true));
            sobe.Add(new Soba(2, tipsobe2, true, true));
            sobe.Add(new Soba(3, tipsobe2, true, false));
            sobe.Add(new Soba(4, tipoviSobe[0], false, true));
            sobe.Add(new Soba(1, tipoviSobe[1], false, false));
            //iznajmljivanja.Add(new Iznajmljivanje(Iznajmljivanje.IzabraniTip.DnevniBoravak,new ObservableCollection<Gost>(),new Soba(1, tipsobe, true, true),new DateTime(2000,2,2),new DateTime(2000,2,4),500));

        }

        public void LoadData()
        {
            KorisnikDAO.Load();
            TipSobeDAO.Load();
            SobaDAO.Load();
            TipIznajmljivanjaDAO.Load();
            CenovnikDAO.Load();
            IznajmljivanjeDAO.Load();
            IznajmljivanjeDAO.LoadBezObziraDaLiPostoje();
            GostDAO.Load();
            
        }

        public Soba PronadjiSobuPoId(long id)
        {
            foreach (Soba s in this.sobe)
            {
                if (s.Id == id)
                {
                    return s;
                }
            } return null;
        }

        public Korisnik PronadjiUlogovanog(string korisnickoIme, string lozinka)
        {
            foreach (Korisnik k in Aplikacija.korisnici)
            {
                if (k.KorisnickoIme == korisnickoIme & k.Lozinka == lozinka)
                {
                    return k;
                }
            } return null;
        }

        public TipSobe PronadjiTipSobePoId(long id)
        {
            foreach (TipSobe tipSobe in this.tipoviSobe)
            {
                if (tipSobe.Id == id)
                {
                    return tipSobe;
                }
            } return null;
        }

        public Soba PronadjiSobuPoBroju(int broj)
        {
            foreach (Soba s in this.sobe)
            {
                if (s.Broj == broj)
                {
                    return s;
                }
            } return null;
        }

        public TipIznajmljivanja PronadjiTipIznajmljivanja(long id)
        {
            foreach (TipIznajmljivanja ti in this.tipoviIznajmljivanja)
            {
                if (ti.Id == id)
                {
                    return ti;
                }
            } return null;
        }

        public ObservableCollection<Iznajmljivanje> PronadjiIznajmljivanjaPoTipu(TipIznajmljivanja tip)
        {
            ObservableCollection<Iznajmljivanje> novaKolekcija = new ObservableCollection<Iznajmljivanje>();
            foreach (Iznajmljivanje i in this.iznajmljivanja)
            {
                if (i.TipIznajmljivanja.Naziv == tip.Naziv)
                {
                    novaKolekcija.Add(i);
                }
            } return novaKolekcija;
        }

        public ObservableCollection<Iznajmljivanje> PronadjiIznajmljivanjaPoBrojuSobe(int brSobe)
        {
            ObservableCollection<Iznajmljivanje> novaKolekcija = new ObservableCollection<Iznajmljivanje>();
            foreach (Iznajmljivanje i in this.iznajmljivanja)
            {
                if (i.Soba.Broj == brSobe)
                {
                    novaKolekcija.Add(i);
                }
            } return novaKolekcija;
        }

        public ObservableCollection<Iznajmljivanje> PronadjiIznajmljivanjaPoDatumuDolaska(string datumString)
        {
            ObservableCollection<Iznajmljivanje> novaKolekcija = new ObservableCollection<Iznajmljivanje>();
            DateTime datum = DateTime.Parse(datumString);
            foreach (Iznajmljivanje i in this.iznajmljivanja)
            {
                if (i.PocetniDatum == datum)
                {
                    novaKolekcija.Add(i);
                }
            } return novaKolekcija;
        }

        public ObservableCollection<Iznajmljivanje> PronadjiIznajmljivanjaPoDatumuOdlaska(string datumString)
        {
            ObservableCollection<Iznajmljivanje> novaKolekcija = new ObservableCollection<Iznajmljivanje>();
            DateTime datum = DateTime.Parse(datumString);
            foreach (Iznajmljivanje i in this.iznajmljivanja)
            {
                if (i.ZavrsniDatum == datum)
                {
                    novaKolekcija.Add(i);
                }
            } return novaKolekcija;
        }

        public ObservableCollection<Iznajmljivanje> PrikaziAktuelnaIznajmljivanja()
        {
            ObservableCollection<Iznajmljivanje> novaKolekcija = new ObservableCollection<Iznajmljivanje>();
            foreach (Iznajmljivanje i in this.iznajmljivanja)
            {
                if (i.PocetniDatum <= DateTime.Today && i.ZavrsniDatum >= DateTime.Today)
                {
                    novaKolekcija.Add(i);
                }
            } return novaKolekcija;
        }

        public Iznajmljivanje PronadjiIznajmljivanjaPoID(long id)
        {
            foreach (Iznajmljivanje i in this.iznajmljivanja)
            {
                if (i.Id == id)
                {
                    return i;
                }
            } return null;
        }

        public decimal PronadjiCenu(TipIznajmljivanja izn, Soba soba)
        {
            foreach (Cenovnik cene in this.cenovnik)
            {
                if (izn.Naziv == cene.TipIznajmljivanja.Naziv && soba.TipS.Naziv == cene.TipS.Naziv)
                {
                    return cene.Cena;
                }
            } return 0;
        }

        public ObservableCollection<Gost> prikaziSveGoste()
        {
            ObservableCollection<Gost> sviGosti = new ObservableCollection<Gost>();
            foreach (Iznajmljivanje izn in this.iznajmljivanja)
            {
                foreach (Gost g in izn.Gosti)
                {
                    sviGosti.Add(g);
                }
            }
            return sviGosti;
        }

        public void upisiZauzeteSobe()
        {
            foreach (Iznajmljivanje iznajmljivanje in Aplikacija.Instanca.hotel.iznajmljivanja)
            {
                if (iznajmljivanje.PocetniDatum <= DateTime.Today && iznajmljivanje.ZavrsniDatum >= DateTime.Today)
                {
                    iznajmljivanje.Soba.JeZauzeta = true;
                }
            }
        }

        public ObservableCollection<Soba> prikaziSlobodneSobe()
        {
            ObservableCollection<Soba> slobodneSobe = new ObservableCollection<Soba>();
            foreach (Soba s in Aplikacija.Instanca.hotel.sobe)
            {
                if (s.JeZauzeta == false)
                {
                    slobodneSobe.Add(s);
                }
            }

            return slobodneSobe;
        }

        public ObservableCollection<Soba> prikaziZauzeteSobe()
        {
            ObservableCollection<Soba> zauzeteSobe = new ObservableCollection<Soba>();
            foreach (Soba s in Aplikacija.Instanca.hotel.sobe)
            {
                if (s.JeZauzeta == true)
                {
                    zauzeteSobe.Add(s);
                }
            }

            return zauzeteSobe;
        }
        


        //-----------------nije dobra funkcija.................
        public ObservableCollection<TipSobe> prikaziSlobodneTipoveSobe_za_cenovnik()
        {
            ObservableCollection<TipSobe> slobodniTipoviSobe = new ObservableCollection<TipSobe>();
            foreach (TipSobe ts in this.tipoviSobe)
            {
                foreach (Cenovnik c in this.cenovnik)
                {
                    if (this.PronadjiTipSobePoId(c.TipS.Id) != ts)
                    {
                        slobodniTipoviSobe.Add(ts);
                    }
                }
            }
            return slobodniTipoviSobe;
        }

        public bool da_li_postoji_gost(Gost g)
        {
            foreach (Iznajmljivanje izn in iznajmljivanja)
            {
                if (g.IdIznajmljivanja == izn.Id)
                {
                    return true;
                }
            }
            return false;
        }

        /*
        private void ucitajKorisnikeIzFajla()
        {
            using (StreamReader sr = new StreamReader(@"..\..\korisnici.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] kor = line.Split("\\|".ToCharArray());

                    string ime = kor[0];
                    string prezime = kor[1];
                    string jmbg = kor[2];
                    string korIme = kor[3];
                    string lozinka = kor[4];
                    string tipKorisnika = kor[5];

                    Korisnik korisnik = new Korisnik(ime,prezime,jmbg,korIme,lozinka,tipKorisnika);
                    korisnici.Add(korisnik);


                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine(line);
                }

            };
        }

         
        private void ucitajSobeIzFajla()
        {
            using (StreamReader sr = new StreamReader(@"..\..\sobe.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(new char[] { '|' });

                    string brojStr = s[0];
                    int broj = Int32.Parse(brojStr);
                    string naziv = s[1];
                    string brojKrevetaStr = s[2];
                    int brojKreveta = Int32.Parse(brojKrevetaStr);
                    TipSobe tipSobe = new TipSobe(naziv, brojKreveta);
                    string tvStr = s[3];
                    bool tv = Boolean.Parse(tvStr);
                    string miniBarStr = s[4];
                    bool miniBar = Boolean.Parse(miniBarStr);

                    Soba soba1 = new Soba(broj, tipSobe, tv, miniBar);
                    sobe.Add(soba1);


                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine(line);
                }

            };
        }

        
        private static void upisiSobeUFajl()
        {

            using (StreamWriter sw = new StreamWriter(@"..\..\sobe.txt", true))
            {

                foreach (Soba s in sobe)
                {
                    sw.WriteLine((string)s.Broj);
                    sw.WriteLine("|");
                    sw.WriteLine(s.naziv);
                    sw.WriteLine("|");
                    sw.WriteLine((string)s.brojKreveta);
                    sw.WriteLine("|");
                    sw.WriteLine((string)s.tv);
                    sw.WriteLine("|");
                    sw.WriteLine((string)s.miniBar);
                }
            }

        }

         */

        public Hotel(string naziv, double PIB, string adresa, string vlasnik)
        {
            sobe = new ObservableCollection<Soba>();
            korisnici = new ObservableCollection<Korisnik>();
            this.naziv = naziv;
            this.PIB = PIB;
            this.adresa = adresa;
            this.vlasnik = vlasnik;
        }
    }
}
