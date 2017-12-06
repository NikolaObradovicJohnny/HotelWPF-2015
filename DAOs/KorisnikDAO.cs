using NoviHotelWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.DAOs
{
    class KorisnikDAO
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from korisnik where postoji=1";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "korisnici");

                foreach (DataRow row in ds.Tables["korisnici"].Rows)
                {
                    Korisnik k = new Korisnik();
                    k.Id = (long)row["ID"];
                    k.Ime = (string)row["IME"];
                    k.Prezime = (string)row["PREZIME"];
                    k.JMBG = (string)row["JMBG"];
                    k.KorisnickoIme = (string)row["KORISNICKO_IME"];
                    k.Lozinka = (string)row["LOZINKA"];
                    k.TipKorisnika = (Korisnik.TIP_KORISNIKA)row["TIP_KORISNIKA"];

                    Aplikacija.korisnici.Add(k);

                }


            }
        }

        public static void Create(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"insert into korisnik (ime,prezime,jmbg,korisnicko_ime,lozinka,tip_korisnika,postoji) values (@ime, @prezime, @jmbg, @korisnickoIme, @lozinka, @tipKorisnika, 1)";


                cmd.Parameters.Add(new SqlParameter("@ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@jmbg", k.JMBG));
                cmd.Parameters.Add(new SqlParameter("@korisnickoIme", k.KorisnickoIme));
                cmd.Parameters.Add(new SqlParameter("@lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@tipKorisnika", k.TipKorisnika));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Edit(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update korisnik set ime=@ime, prezime=@prezime,jmbg=@jmbg,korisnicko_ime=@korisnickoIme,lozinka=@lozinka,tip_korisnika=@tipKorisnika where id=@id";

                cmd.Parameters.Add(new SqlParameter("@ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@jmbg", k.JMBG));
                cmd.Parameters.Add(new SqlParameter("@korisnickoIme", k.KorisnickoIme));
                cmd.Parameters.Add(new SqlParameter("@lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@tipKorisnika", k.TipKorisnika));
                cmd.Parameters.Add(new SqlParameter("@id", k.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Delete(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete korisnik where id=@id";


                cmd.Parameters.Add(new SqlParameter("@id", k.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void DeleteLogicko(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update korisnik set postoji=@postoji where id=@id";

                cmd.Parameters.Add(new SqlParameter("@postoji", false));
                cmd.Parameters.Add(new SqlParameter("@id", k.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Search(String uneto)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from korisnik where korisnicko_ime like '%@uneto%' ";


                cmd.Parameters.Add(new SqlParameter("@uneto", uneto));

                cmd.ExecuteReader();

            }
        }
    }
}
