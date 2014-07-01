using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entiteti
{
    public class MANEKEN
    {
        // Primary key
        public virtual int MATICNI_BROJ { get; set; }

        // Attributes
        public virtual string IME { get; set; }
        public virtual string PREZIME { get; set; }
        public virtual char POL { get; set; }
        public virtual DateTime DATUM_RODJENJA { get; set; }
        public virtual int VISINA { get; set; }
        public virtual int TEZINA { get; set; }
        public virtual string BOJA_KOSE { get; set; }
        public virtual string BOJA_OCIJU { get; set; }
        public virtual int KONFEKCIJSKI_BROJ { get; set; }
        public virtual int SPECIJALNI_GOST_FLAG { get; set; }

        public virtual string IME_PREZIME
        {
            get
            {
                return IME + " " + PREZIME;
            }
        }
        
        // Multi-valued attribute
        public virtual IList<CASOPIS> casopisi { get; set; }

        // Many-to-one relationship
        public virtual MODNA_AGENCIJA modnaAgencija { get; set; }

        // Many-to-many relationship
        public virtual IList<MODNA_REVIJA> modneRevije { get; set; }
        
        public MANEKEN()
        {
            casopisi = new List<CASOPIS>();
            modneRevije = new List<MODNA_REVIJA>();
        }
    }
}
