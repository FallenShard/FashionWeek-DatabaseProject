using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Entiteti
{
    public class INTERNACIONALNA_AGENCIJA : MODNA_AGENCIJA
    {
        // Multi-valued attribute
        public virtual IList<DRZAVA> drzave { get; set; }

        public INTERNACIONALNA_AGENCIJA()
        {
            drzave = new List<DRZAVA>();
        }
    }
}
