using NoviHotelWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.DAOs
{
    class IznajmljivanjeDAO
    {
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from iznajmljivanje where postoji=1";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "iznajmljivanja");

                foreach (DataRow row in ds.Tables["iznajmljivanja"].Rows)
                {
                    Iznajmljivanje i = new Iznajmljivanje();
                    i.Gosti = new ObservableCollection<Gost>();
                    i.Id = (long)row["ID"];
                    i.Soba = Aplikacija.Instanca.hotel.PronadjiSobuPoId((long)row["SOBA_ID"]);
                    i.PocetniDatum = (DateTime)row["POCETNI_DATUM"];
                    i.ZavrsniDatum = (DateTime)row["ZAVRSNI_DATUM"];
                    i.TipIznajmljivanja = Aplikacija.Instanca.hotel.PronadjiTipIznajmljivanja((long)row["TIP_IZNAJMLJIVANJA_ID"]);
                    //i.UkupnaCena = (decimal)row["UKUPNA_CENA"];

                    i.UkupnaCena = i.IzracunajUkupnuCenu();

                    SqlCommand cmdCena = conn.CreateCommand();
                    cmdCena.CommandText = @"update iznajmljivanje set ukupna_cena=@ukupnaCena where id=@id";

                    cmdCena.Parameters.Add(new SqlParameter("@ukupnaCena", i.UkupnaCena));
                    cmdCena.Parameters.Add(new SqlParameter("@id", i.Id));

                    cmdCena.ExecuteNonQuery();

                    Aplikacija.Instanca.hotel.iznajmljivanja.Add(i);

                }
            }
        }

        public static void Create(Iznajmljivanje i)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"insert into iznajmljivanje (soba_id,tip_iznajmljivanja_id,pocetni_datum,zavrsni_datum,ukupna_cena,postoji) values (@sobaId, @tipIznajmljivanjaId, @pocDatum, @zavDatum, @ukupnaCena, @postoji)";

                i.IzracunajUkupnuCenu();
                cmd.Parameters.Add(new SqlParameter("@sobaId", i.Soba.Id));
                cmd.Parameters.Add(new SqlParameter("@tipIznajmljivanjaId", i.TipIznajmljivanja.Id));
                cmd.Parameters.Add(new SqlParameter("@pocDatum", i.PocetniDatum));
                cmd.Parameters.Add(new SqlParameter("@zavDatum", i.ZavrsniDatum));
                cmd.Parameters.Add(new SqlParameter("@ukupnaCena",i.UkupnaCena));
                cmd.Parameters.Add(new SqlParameter("@postoji", true));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Update(Iznajmljivanje i)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update iznajmljivanje set soba_id=@sobaId, tip_iznajmljivanja_id=@tipIznajmljivanjaId,pocetni_datum=@pocDatum,zavrsni_datum=@zavDatum,ukupna_cena=@ukupnaCena where id=@id";

                cmd.Parameters.Add(new SqlParameter("@sobaId", i.Soba.Id));
                cmd.Parameters.Add(new SqlParameter("@tipIznajmljivanjaId", i.TipIznajmljivanja.Id));
                cmd.Parameters.Add(new SqlParameter("@pocDatum", i.PocetniDatum));
                cmd.Parameters.Add(new SqlParameter("@zavDatum", i.ZavrsniDatum));
                cmd.Parameters.Add(new SqlParameter("@ukupnaCena", i.UkupnaCena));
                cmd.Parameters.Add(new SqlParameter("@id", i.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public static void Delete(Iznajmljivanje i)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"update iznajmljivanje set postoji=0 where id=@id";

                cmd.Parameters.Add(new SqlParameter("@id", i.Id));

                cmd.ExecuteNonQuery();

            }
        }

        public void PretragaPoTipu(TipIznajmljivanja ti)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from iznajmljivanje where tip_iznajmljivanja_id = @id";

                cmd.Parameters.Add(new SqlParameter("@id", ti.Id));

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "iznajmljivanja");

            }
        }

        public void PretragaPoBrojuSobe(Soba s)
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from iznajmljivanje where soba_id in (select id from soba where broj = @broj)";

                cmd.Parameters.Add(new SqlParameter("@broj", s.Broj));

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "iznajmljivanja");

            }
        }

        public static void LoadBezObziraDaLiPostoje()
        {
            using (SqlConnection conn = new SqlConnection(Aplikacija.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"select * from iznajmljivanje";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "iznajmljivanja");

                foreach (DataRow row in ds.Tables["iznajmljivanja"].Rows)
                {
                    Iznajmljivanje i = new Iznajmljivanje();
                    i.Gosti = new ObservableCollection<Gost>();
                    i.Id = (long)row["ID"];
                    i.Soba = Aplikacija.Instanca.hotel.PronadjiSobuPoId((long)row["SOBA_ID"]);
                    i.PocetniDatum = (DateTime)row["POCETNI_DATUM"];
                    i.ZavrsniDatum = (DateTime)row["ZAVRSNI_DATUM"];
                    i.TipIznajmljivanja = Aplikacija.Instanca.hotel.PronadjiTipIznajmljivanja((long)row["TIP_IZNAJMLJIVANJA_ID"]);
                    //i.UkupnaCena = (decimal)row["UKUPNA_CENA"];

                    i.UkupnaCena = i.IzracunajUkupnuCenu();

                    SqlCommand cmdCena = conn.CreateCommand();
                    cmdCena.CommandText = @"update iznajmljivanje set ukupna_cena=@ukupnaCena where id=@id";

                    cmdCena.Parameters.Add(new SqlParameter("@ukupnaCena", i.UkupnaCena));
                    cmdCena.Parameters.Add(new SqlParameter("@id", i.Id));

                    cmdCena.ExecuteNonQuery();

                    Aplikacija.Instanca.hotel.posete_bezObziraDaLiPostojeIliNe.Add(i);

                }
            }
        }

    }
}
