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
    class TipIznajmljivanjaDAO
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from tip_iznajmljivanja";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "tipoviIznajmljivanja");

                //Console.WriteLine(ds.GetXml());
                foreach (DataRow row in ds.Tables["tipoviIznajmljivanja"].Rows)
                {
                    TipIznajmljivanja ti = new TipIznajmljivanja();
                    ti.Id = (long)row["ID"];
                    ti.Naziv = (string)row["NAZIV"];

                    Aplikacija.Instanca.hotel.tipoviIznajmljivanja.Add(ti);

                }
            }
        }
    }
}
