using Autofac;
using Autofac.Core;
using OrderingSystem.Business;
using OrderingSystem.Core.Data;
using OrderingSystem.Core.Infrastructure.DependencyManagement;
using OrderingSystem.Data;
using OrderingSystem.Data.Repositories;
using OrderingSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business.Tests
{
    class DependencyRegister : IDependencyRegistrar
    {
        public void Register(Autofac.ContainerBuilder builder, Core.Infrastructure.TypeFinders.ITypeFinder typeFinder)
        {
            #region 数据库

            const string MAIN_DB = "OrderingSystem";

            builder.Register(c => new OrderingSystemDbContext(MAIN_DB))
                    .As<IDbContext>()
                    .Named<IDbContext>(MAIN_DB)
                    .SingleInstance();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(MAIN_DB))
                .SingleInstance();

            #endregion

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IUserBusiness))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
        }
        public int Order
        {
            get { return 1; }
        }
    }
}
