﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


using Autofac;
using Autofac.Integration.Mvc;

using Ioc;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = RegisterService();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }


        private ContainerBuilder RegisterService()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();

            builder.RegisterControllers(assemblys.ToArray());

            builder.RegisterAssemblyTypes(assemblys.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerRequest();
            return builder;
        }
    }
}
