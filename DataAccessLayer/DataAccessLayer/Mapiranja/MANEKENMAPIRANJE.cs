using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class MANEKENMAPIRANJE : ClassMap<MANEKEN>
    {
        public MANEKENMAPIRANJE()
        {
            // Primary key mapping
            Id(x => x.MATICNI_BROJ).GeneratedBy.Assigned();

            // Attributes mapping
            Map(x => x.IME);
            Map(x => x.PREZIME);
            Map(x => x.POL);
            Map(x => x.DATUM_RODJENJA);
            Map(x => x.VISINA);
            Map(x => x.TEZINA);
            Map(x => x.BOJA_KOSE);
            Map(x => x.BOJA_OCIJU);
            Map(x => x.KONFEKCIJSKI_BROJ);
            //Map(x => x.SPECIJALNI_GOST_FLAG);
            
            // Multi-value attribute mapping
            HasMany(x => x.casopisi).Cascade.AllDeleteOrphan().Inverse().KeyColumn("MATICNI_BROJ_M");

            // Many-to-one relationship mapping
            References(x => x.modnaAgencija, "MODNA_AGENCIJA_PIB");

            // Many-to-many relationship mapping
            HasManyToMany(x => x.modneRevije).Table("NASTUPA").
                ParentKeyColumn("MATICNI_BROJ_M").ChildKeyColumn("REDNI_BROJ_R").Cascade.All();
            
            // Discrimnator flag for this class - 0
            DiscriminateSubClassesOnColumn("SPECIJALNI_GOST_FLAG", 0);
        }
    }
}
