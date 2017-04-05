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
using zzbj.bll;
using zzbj.core;
using zzbj.dal;
using zzbj.ibll;
using zzbj.iBll;
using zzbj.idal;
using zzbj.models;
using zzbj.repository;

namespace zzbj.uis
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var builder = new ContainerBuilder();
            //SetupResolveRules(builder);
            InjectModule.LoadInjectModule(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            CommonContainer.kernel = container;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Dal<>)).As(typeof(IDal<>))
        .InstancePerDependency();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                .InstancePerDependency();
            builder.RegisterType<personBll>().As<IpersonBll>();
            builder.RegisterType<customBll>().As<IcustomBll>();
            //builder.RegisterType<bas_userBll>().As<Ibas_userBll>();
            //builder.Register(c => new customBll((IRepository<custom>)
            //    c.Resolve(typeof(IRepository<custom>))));
        }
    }
}
