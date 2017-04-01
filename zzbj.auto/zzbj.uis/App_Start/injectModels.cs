using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using zzbj.bll;
using zzbj.dal;
using zzbj.ibll;
using zzbj.idal;
using zzbj.repository;

namespace zzbj.uis.App_Start
{
    public class injectModels
    {
        public injectModels(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Dal<>)).As(typeof(IDal<>))
.InstancePerDependency();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                .InstancePerDependency();
            builder.RegisterType<personBll>().As<IpersonBll>();
            builder.RegisterType<customBll>().As<IcustomBll>();
            //builder.Register(c => new personBll((IRepository<person>)
            //    c.Resolve(typeof(IRepository<person>))));
            //builder.Register(c => new customBll((IRepository<custom>)
            //    c.Resolve(typeof(IRepository<custom>))));
        }
    }
}