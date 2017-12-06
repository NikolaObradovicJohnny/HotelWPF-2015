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
    class TipSobeDAO
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from tip_soba where postoji=1";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "tipoviSobe");

                //Console.WriteLine(ds.GetXml());
                foreach (DataRow row in ds.Tables["tipoviSobe"].Rows)
                {
                    TipSobe ts = new TipSobe();
                    ts.Id = (long)row["ID"];
                    ts.Naziv = (string)row["NAZIV"];
                    ts.BrojKreveta = (int)row["BR_KREVETA"];

                    Aplikacija.Instanca.hotel.tipoviSobe.Add(ts);

                }


            }
        }

        public static void Create(TipSobe ts)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"insert into tip_soba (br_kreveta,naziv,postoji) values (@brKreveta, @naziv, 1)";

                cmd.Parameters.Add(new SqlParameter("@brKreveta", ts.BrojKreveta));
                cmd.Parameters.Add(new SqlParameter("@naziv", ts.Naziv));
                cmd.Parameters.Add(new SqlParameter("@id", ts.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Update(TipSobe ts)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update tip_soba set br_kreveta=@brKreveta, naziv=@naziv where id=@id";

                cmd.Parameters.Add(new SqlParameter("@brKreveta", ts.BrojKreveta));
                cmd.Parameters.Add(new SqlParameter("@naziv", ts.Naziv));
                cmd.Parameters.Add(new SqlParameter("@id", ts.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Delete(TipSobe ts)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete tip_soba where id=@id";


                cmd.Parameters.Add(new SqlParameter("@id", ts.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void DeleteLogicko(TipSobe ts)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update tip_soba set postoji=0 where id=@id";


                cmd.Parameters.Add(new SqlParameter("@id", ts.Id));

                cmd.ExecuteNonQuery();

            }
        }
    }
}
