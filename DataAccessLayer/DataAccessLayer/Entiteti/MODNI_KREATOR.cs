using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entiteti
{
    public class MODNI_KREATOR
    {
        // Primary key
        public virtual int MATICNI_BROJ { get; set; }

        // Attributes
        public virtual string IME { get; set; }
        public virtual string PREZIME { get; set; }
        public virtual char POL { get; set; }
        public virtual DateTime DATUM_RODJENJA { get; set; }
        public virtual string ZEMLJA_POREKLA { get; set; }
        public virtual string MODNA_KUCA { get; set; }

        public virtual string IME_PREZIME
        {
            get
            {
                return IME + " " + PREZIME;
            }
        }

        
        // Many-to-many relationship
        public virtual IList<MODNA_REVIJA> modneRevije { get; set; }
         

        public MODNI_KREATOR()
        {
            modneRevije = new List<MODNA_REVIJA>();
        }
    }
}
