using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class SPECIJALNI_GOSTMAPIRANJE : SubclassMap<SPECIJALNI_GOST>
    {
        public SPECIJALNI_GOSTMAPIRANJE()
        {
            // Discriminator value for this subclass - 1
            DiscriminatorValue(1);

            // Primary key
            KeyColumn("MATICNI_BROJ");
            
            // Attribute mapping
            Map(x => x.ZANIMANJE);
        }
    }
}
