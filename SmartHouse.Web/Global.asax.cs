﻿using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using SmartHouse.Services.UnitOfWork;

namespace SmartHouse.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnitOfWorkFactory.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            logger.Error(exception);
        }
    }
}
