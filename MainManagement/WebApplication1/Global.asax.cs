using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


using Autofac;
using Autofac.Integration.Mvc;
using BLL;
using Common;
using Common.NLog;
using Common.UnitOfWork;
using IBLL;
using IDAL;
using Ioc;
using Model;
using DAL;

namespace SiteWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperHelper.MapObject();
            var builder = RegisterService();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }


        private ContainerBuilder RegisterService()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            builder.RegisterControllers(assemblys.ToArray());
            builder.RegisterType<LogFactory>().As<ILogFactory>().SingleInstance();
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            //builder.RegisterType<ContactDal>().As<IContactDAL>().InstancePerLifetimeScope();
            //builder.RegisterType<ContactService>().As<IContactService>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
