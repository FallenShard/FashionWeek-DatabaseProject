using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    class INTERNACIONALNA_AGENCIJAMAPIRANJE : SubclassMap<INTERNACIONALNA_AGENCIJA>
    {
        public INTERNACIONALNA_AGENCIJAMAPIRANJE()
        {
            // Discriminator value for this subclass - 1
            DiscriminatorValue(1);

            // Primary key
            KeyColumn("PIB");
            
            // One-to-many (multi-value attribute) mapping (not the owner)
            HasMany(x => x.drzave).Cascade.AllDeleteOrphan().Inverse().KeyColumn("AGENCIJA_PIB");
        }
    }
}
