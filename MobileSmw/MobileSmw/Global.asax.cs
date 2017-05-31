using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using IOC;

namespace MobileSmw
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperHelper.Map();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(RegisterService().Build()));
        }

        /// <summary>
        /// IOC注入
        /// </summary>
        /// <returns></returns>
        private ContainerBuilder RegisterService()
        {
            var builder = new ContainerBuilder();
            var assemblies = new[]
            {
                Assembly.Load("Common"),
                Assembly.Load("DAL"),
                Assembly.Load("BLL")
            };
            var baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterControllers(Assembly.Load("MobileSmw"));
            return builder;
        }
    }
}
