using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class CASOPISMAPIRANJE : ClassMap<CASOPIS>
    {
        public CASOPISMAPIRANJE()
        {
            // Composite primary key mapping
            CompositeId().KeyReference(x => x.maneken, "MATICNI_BROJ_M")
                         .KeyProperty(x => x.NASLOV_CASOPISA);
        }
    }
}
