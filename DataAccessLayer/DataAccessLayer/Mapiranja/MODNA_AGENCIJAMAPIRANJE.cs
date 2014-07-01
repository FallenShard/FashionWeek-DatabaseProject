using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class MODNA_AGENCIJAMAPIRANJE : ClassMap<MODNA_AGENCIJA>
    {
        public MODNA_AGENCIJAMAPIRANJE()
        {
            // Primary key mapping
            Id(x => x.PIB).GeneratedBy.Assigned();

            // Attributes mapping
            Map(x => x.NAZIV);
            Map(x => x.SEDISTE);
            Map(x => x.DATUM_OSNIVANJA);
            // Map(x => x.INTERNACIONALNA_FLAG);

            // Discriminator value for this class - -1
            DiscriminateSubClassesOnColumn("INTERNACIONALNA_FLAG", -1);

            // One-to-many relationship mapping
            HasMany(x => x.manekeni).Inverse().Cascade.All().KeyColumn("MODNA_AGENCIJA_PIB");
        }
    }
}
