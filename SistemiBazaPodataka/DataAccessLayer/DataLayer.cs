using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using DataAccessLayer.Mapiranja;

namespace DataAccessLayer
{
    public class DataLayer
    {
        private static ISessionFactory m_factory = null;

        // Lazy init of the session factory to conserve resources
        public static ISession GetSession()
        {
            if (m_factory == null)
            {
                m_factory = CreateSessionFactory();
            }

            return m_factory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var cfg = OracleClientConfiguration.Oracle10
            .ConnectionString(c =>
                c.Is("DATA SOURCE=160.99.9.199:1521/gislab.elfak.ni.ac.rs;" + 
                     "PERSIST SECURITY INFO=True;" + 
                     "USER ID=S13904;" + 
                     "PASSWORD=S13904"));

            return Fluently.Configure()
                .Database(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MODNI_KREATORMAPIRANJE>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }
    }
}
