using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace Swinkaran.Nhbnt.Web
{
    public class NhibernateSession
    {
        private static ISessionFactory _sessionFactory;
        public static ISession OpenSession()
        {
            Configuration configuration1 = new NHibernate.Cfg.Configuration();
            string configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            configuration1.Configure(configurationPath);
            string bookConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Book.hbm.xml");
            configuration1.AddFile(bookConfigurationFile);
            ISessionFactory sessionFactory = configuration1.BuildSessionFactory();
            return sessionFactory.OpenSession();

        }

    }
}