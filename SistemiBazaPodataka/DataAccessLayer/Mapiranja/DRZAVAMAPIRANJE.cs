using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class DRZAVAMAPIRANJE : ClassMap<DRZAVA>
    {

        public DRZAVAMAPIRANJE()
        {
            // Composite primary key mapping
            CompositeId().KeyReference(x => x.int_agencija, "AGENCIJA_PIB")
                         .KeyProperty(x => x.NAZIV_DRZAVE);
        }
    }
}
