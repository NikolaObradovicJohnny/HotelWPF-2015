using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoviHotelWPF.Model
{
    public class TipIznajmljivanja
    {
        private long id;
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }


        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public TipIznajmljivanja() { }

        public TipIznajmljivanja(long id, string naziv)
        {
            this.Id = id;
            this.Naziv = naziv;
        }

        public override string ToString()
        {
            return this.Naziv;
        }
    }
}
