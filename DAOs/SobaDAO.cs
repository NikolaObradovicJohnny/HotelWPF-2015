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
    public class SobaDAO // : IDisposable
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from soba where postoji=1";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "sobe");

                //Console.WriteLine(ds.GetXml());
                foreach (DataRow row in ds.Tables["sobe"].Rows)
                {
                    Soba s = new Soba();
                    s.Id = (long)row["ID"];
                    s.Broj = (int)row["BROJ"];
                    //s.TipS = row["TIP_ID"] as TipSobe;
                    s.TipS = Aplikacija.Instanca.hotel.PronadjiTipSobePoId((long)row["TIP_ID"]);
                    s.Tv = (bool)row["TV"];
                    s.MiniBar = (bool)row["MINI_BAR"];

                    Aplikacija.Instanca.hotel.sobe.Add(s);

                }


            }
        }

        public static void Create(Soba s)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"insert into soba (broj,tip_id,tv,mini_bar,postoji) values (@broj, @tip, @tv, @miniBar, 1)";

                cmd.Parameters.Add(new SqlParameter("@broj", s.Broj));
                cmd.Parameters.Add(new SqlParameter("@tip", s.TipS.Id));
                cmd.Parameters.Add(new SqlParameter("@tv", s.Tv));
                cmd.Parameters.Add(new SqlParameter("@miniBar", s.MiniBar));
                cmd.Parameters.Add(new SqlParameter("@id", s.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Update(Soba s)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update soba set broj=@broj, tip_id=@tip,tv=@tv,mini_bar=@miniBar where id=@id";

                cmd.Parameters.Add(new SqlParameter("@broj", s.Broj));
                cmd.Parameters.Add(new SqlParameter("@tip", s.TipS.Id));
                cmd.Parameters.Add(new SqlParameter("@tv", s.Tv));
                cmd.Parameters.Add(new SqlParameter("@miniBar", s.MiniBar));
                cmd.Parameters.Add(new SqlParameter("@id", s.Id));

                cmd.ExecuteNonQuery();

            }
        }
        //fizicko brisanje............
        public static void Delete(Soba s)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete soba where id=@id";


                cmd.Parameters.Add(new SqlParameter("@id", s.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void DeleteLogicko(Soba s)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update soba set postoji=@postoji where id=@id";

                cmd.Parameters.Add(new SqlParameter("@postoji", false));
                cmd.Parameters.Add(new SqlParameter("@id", s.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Search(String uneto)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from soba where tip_id in (select id from tip_soba where naziv=@uneto )";


                cmd.Parameters.Add(new SqlParameter("@uneto", uneto));

                cmd.ExecuteNonQuery();
            }
        }
    }
}
