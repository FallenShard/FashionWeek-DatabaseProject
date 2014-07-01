using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entiteti
{
    public class MODNA_AGENCIJA
    {
        // Primary key
        public virtual int PIB { get; set; }

        // Attributes
        public virtual string NAZIV { get; set; }
        public virtual string SEDISTE { get; set; }
        public virtual DateTime DATUM_OSNIVANJA { get; set; }

        // Discriminator flag
        public virtual short INTERNACIONALNA_FLAG { get; set; }

        // One-to-many relationship
        public virtual IList<MANEKEN> manekeni { get; set; }

        public MODNA_AGENCIJA()
        {
            manekeni = new List<MANEKEN>();
        }
    }
}
