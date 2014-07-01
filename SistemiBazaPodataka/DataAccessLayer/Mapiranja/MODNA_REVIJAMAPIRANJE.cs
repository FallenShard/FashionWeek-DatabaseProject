using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class MODNA_REVIJAMAPIRANJE : ClassMap<MODNA_REVIJA>
    {
        public MODNA_REVIJAMAPIRANJE()
        {
            // Primary key mapping - by trigger
            Id(x => x.REDNI_BROJ).GeneratedBy.TriggerIdentity();

            // Attributes mapping
            Map(x => x.NAZIV);
            Map(x => x.MESTO);
            Map(x => x.DATUM_VREME);
            
            // Many-to-many foreign keys mapping
            HasManyToMany(x => x.modniKreatori).Table("PREDSTAVLJA").
                ParentKeyColumn("REDNI_BROJ_R").ChildKeyColumn("MATICNI_BROJ_K").Cascade.All().Inverse();

            HasManyToMany(x => x.manekeni).Table("NASTUPA").
                ParentKeyColumn("REDNI_BROJ_R").ChildKeyColumn("MATICNI_BROJ_M").Cascade.All().Inverse();
        }
    }
}
