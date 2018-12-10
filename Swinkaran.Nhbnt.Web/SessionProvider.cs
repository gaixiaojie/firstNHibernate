using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace Swinkaran.Nhbnt.Web
{
    public class SessionProvider
    {
        private static ISessionFactory sessionFactory;
        public static NHibernate.Cfg.Configuration configuration;
        static SessionProvider()
        {
            configuration = new NHibernate.Cfg.Configuration().Configure();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISessionFactory GetSessionFactory()
        {
            return sessionFactory;
        }

        public static ISession GetNewSession()
        {
            return sessionFactory.OpenSession(); //这个方法一般是首次用到Session的时候，然后绑定到上下文中
        }

        public static ISession GetNewOrCurrentSession()
        {
            ISession session = null;
            
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }
            else
            {
                session = sessionFactory.GetCurrentSession();
                if (!session.IsOpen)
                    session = sessionFactory.OpenSession();
            }
            return session;
        }
    }
}