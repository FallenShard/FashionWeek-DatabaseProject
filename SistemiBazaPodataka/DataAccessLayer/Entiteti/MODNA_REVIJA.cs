using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entiteti
{
    public class MODNA_REVIJA
    {
        // Primary key
        public virtual int REDNI_BROJ { get; protected set; }

        // Attributes
        public virtual string NAZIV { get; set; }
        public virtual string MESTO { get; set; }
        public virtual DateTime DATUM_VREME { get; set; }
        
        // Many-to-many relationships
        public virtual IList<MODNI_KREATOR> modniKreatori { get; set; }
        public virtual IList<MANEKEN> manekeni { get; set; }
        
        public MODNA_REVIJA()
        {
            modniKreatori = new List<MODNI_KREATOR>();
            manekeni = new List<MANEKEN>();
        }
    }
}
