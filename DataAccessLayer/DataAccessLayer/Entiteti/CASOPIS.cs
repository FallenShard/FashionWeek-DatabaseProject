using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entiteti
{
    public class CASOPIS
    {
        // Composite primary key, many-to-one multi-valued attribute
        public virtual MANEKEN maneken { get; set; }
        public virtual string NASLOV_CASOPISA { get; set; }
        
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as CASOPIS;
            if (t == null) return false;
            if (maneken == t.maneken && NASLOV_CASOPISA == t.NASLOV_CASOPISA)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            return (maneken.MATICNI_BROJ.ToString() + "|" + NASLOV_CASOPISA).GetHashCode();
        }
        #endregion
        
        public CASOPIS()
        {

        }
    }
}
