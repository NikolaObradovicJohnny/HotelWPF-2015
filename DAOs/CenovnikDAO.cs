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
    class CenovnikDAO
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from cenovnik where postoji=1";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "cenovnik");

                foreach (DataRow row in ds.Tables["cenovnik"].Rows)
                {
                    Cenovnik c = new Cenovnik();
                    c.TipS = Aplikacija.Instanca.hotel.PronadjiTipSobePoId((long)row["TIP_SOBA_ID"]);
                    c.TipIznajmljivanja = Aplikacija.Instanca.hotel.PronadjiTipIznajmljivanja((long)row["TIP_IZNAJMLJIVANJA_ID"]);
                    c.Cena = (decimal)row["CENA"];

                    Aplikacija.Instanca.hotel.cenovnik.Add(c);

                }
            }
        }

        public static void Create(Cenovnik c)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"insert into cenovnik (tip_soba_id,tip_iznajmljivanja_id,cena,postoji) values (@tip_sobe_id, @tip_iznajmljivanja_id, @cena, 1)";

                cmd.Parameters.Add(new SqlParameter("@cena", c.Cena));
                cmd.Parameters.Add(new SqlParameter("@tip_sobe_id", c.TipS.Id));
                cmd.Parameters.Add(new SqlParameter("@tip_iznajmljivanja_id", c.TipIznajmljivanja.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Edit(Cenovnik c)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update cenovnik set cena=@cena where tip_soba_id=@tip_sobe_id and tip_iznajmljivanja_id=@tip_iznajmljivanja_id";

                cmd.Parameters.Add(new SqlParameter("@cena", c.Cena));
                cmd.Parameters.Add(new SqlParameter("@tip_sobe_id", c.TipS.Id));
                cmd.Parameters.Add(new SqlParameter("@tip_iznajmljivanja_id", c.TipIznajmljivanja.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Delete(Cenovnik c)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete cenovnik where tip_soba_id=@tip_sobe_id and tip_iznajmljivanja_id=@tip_iznajmljivanja_id";

                cmd.Parameters.Add(new SqlParameter("@tip_sobe_id", c.TipS.Id));
                cmd.Parameters.Add(new SqlParameter("@tip_iznajmljivanja_id", c.TipIznajmljivanja.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void DeleteLogicko(Cenovnik c)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update cenovnik set postoji=@postoji where tip_soba_id=@tip_sobe_id and tip_iznajmljivanja_id=@tip_iznajmljivanja_id";

                cmd.Parameters.Add(new SqlParameter("@postoji", false));
                cmd.Parameters.Add(new SqlParameter("@@tip_sobe_id", c.TipS.Id));
                cmd.Parameters.Add(new SqlParameter("@tip_iznajmljivanja_id", c.TipIznajmljivanja.Id));

                cmd.ExecuteNonQuery();

            }
        }
        /*
        public static void LoadSlobodne()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from TIP_SOBA A where ID not in (Select TIP_SOBA_ID From CENOVNIK)";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "cenovnik");

                foreach (DataRow row in ds.Tables["cenovnik"].Rows)
                {
                    TipSobe ts = new TipSobe();
                    ts = Aplikacija.Instanca.hotel.PronadjiTipSobePoId((long)row["ID"]);
                    ts.Naziv = ((string)row["NAZIV"]);
                    ts.BrojKreveta = (int)row["BR_KREVETA"];

                    Aplikacija.Instanca.hotel.tipoviSobeKojeNisuU_cenovniku.Add(ts);

                }
            }
        }*/
    }
}
