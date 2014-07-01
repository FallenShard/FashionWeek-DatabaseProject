using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entiteti
{
    public class DRZAVA
    {
        // Composite primary key, many-to-one multi-valued attribute
        public virtual INTERNACIONALNA_AGENCIJA int_agencija { get; set; }
        public virtual string NAZIV_DRZAVE { get; set; }
       
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as DRZAVA;
            if (t == null) return false;
            if (int_agencija == t.int_agencija
             && NAZIV_DRZAVE == t.NAZIV_DRZAVE)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            return (int_agencija.PIB.ToString() + "|" + NAZIV_DRZAVE).GetHashCode();
        }
        #endregion
        
        public DRZAVA()
        {

        }

    }
}
