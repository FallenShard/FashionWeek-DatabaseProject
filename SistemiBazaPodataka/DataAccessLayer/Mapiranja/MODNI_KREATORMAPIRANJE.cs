using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

using DataAccessLayer.Entiteti;

namespace DataAccessLayer.Mapiranja
{
    public class MODNI_KREATORMAPIRANJE : ClassMap<MODNI_KREATOR>
    {
        public MODNI_KREATORMAPIRANJE()
        {
            // Primary key mapping
            Id(x => x.MATICNI_BROJ).GeneratedBy.Assigned();
            
            // Attributes mapping
            Map(x => x.IME);
            Map(x => x.PREZIME);
            Map(x => x.POL);
            Map(x => x.DATUM_RODJENJA);
            Map(x => x.ZEMLJA_POREKLA);
            Map(x => x.MODNA_KUCA);
            
            // Many-to-many relationship mapping
            HasManyToMany(x => x.modneRevije).Table("PREDSTAVLJA").
                ParentKeyColumn("MATICNI_BROJ_K").ChildKeyColumn("REDNI_BROJ_R").Cascade.All();
        }
    }
}
