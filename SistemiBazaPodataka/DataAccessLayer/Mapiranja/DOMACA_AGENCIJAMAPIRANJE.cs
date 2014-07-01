using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    class DOMACA_AGENCIJAMAPIRANJE : SubclassMap<DOMACA_AGENCIJA>
    {
        public DOMACA_AGENCIJAMAPIRANJE()
        {
            // Discriminator value for this subclass - 0
            DiscriminatorValue(0);

            // Primary key
            KeyColumn("PIB");
        }
    }
}
