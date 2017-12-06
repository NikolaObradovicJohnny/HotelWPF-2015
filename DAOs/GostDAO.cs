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
    class GostDAO
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from gost where postoji=1";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "gosti");

                //Console.WriteLine(ds.GetXml());
                foreach (DataRow row in ds.Tables["gosti"].Rows)
                {
                    Gost g = new Gost();
                    g.Id = (long)row["ID"];
                    g.Ime = (string)row["IME"];
                    g.Prezime = (string)row["PREZIME"];
                    g.JMBG = (string)row["JMBG"];
                    g.BrojLicneKarte = (string)row["BR_LICNE_KARTE"];
                    g.IdIznajmljivanja = (long)row["IZNAJMLJIVANJE_ID"];

                    Iznajmljivanje iznajmljivanje = Aplikacija.Instanca.hotel.PronadjiIznajmljivanjaPoID(g.IdIznajmljivanja);


                    iznajmljivanje.Gosti.Add(g);
                    //................................................DODAJ GOSTA U LISTU GOSTIJU.................
                    //Aplikacija.Instanca.hotel.gosti.Add(g);

                }


            }
        }

        public static void Create(Gost g, long iznID)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"insert into gost (ime,prezime,jmbg,br_licne_karte,iznajmljivanje_id,postoji) values (@ime, @prezime, @jmbg, @brLicneKarte, @idIzn, 1)";

                cmd.Parameters.Add(new SqlParameter("@ime", g.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", g.Prezime));
                cmd.Parameters.Add(new SqlParameter("@jmbg", g.JMBG));
                cmd.Parameters.Add(new SqlParameter("@brLicneKarte", g.BrojLicneKarte));
                cmd.Parameters.Add(new SqlParameter("@idIzn", iznID + 1));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Edit(Gost g)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update gost set ime=@ime, prezime=@prezime,jmbg=@jmbg,br_licne_karte=@brLicneKarte,iznajmljivanje_id=@idIzn where id=@id";

                cmd.Parameters.Add(new SqlParameter("@ime", g.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", g.Prezime));
                cmd.Parameters.Add(new SqlParameter("@jmbg", g.JMBG));
                cmd.Parameters.Add(new SqlParameter("@brLicneKarte", g.BrojLicneKarte));
                cmd.Parameters.Add(new SqlParameter("@idIzn", g.IdIznajmljivanja));
                cmd.Parameters.Add(new SqlParameter("@id", g.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Delete(Gost g)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete gost where id=@id";


                cmd.Parameters.Add(new SqlParameter("@id", g.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void DeleteLogicko(Gost g)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update gost set postoji=@postoji where id=@id";

                cmd.Parameters.Add(new SqlParameter("@postoji", false));
                cmd.Parameters.Add(new SqlParameter("@id", g.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Search(String uneto)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from gost where postoji=1 and ( ime like '%@uneto%' or prezime like '%@uneto%' )";


                cmd.Parameters.Add(new SqlParameter("@uneto", uneto));

                cmd.ExecuteNonQuery();
            }
        }
    }
}
