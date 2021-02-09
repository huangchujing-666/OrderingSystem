using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using OrderingSystem.Business;
using OrderingSystem.Core.Data;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Infrastructure.DependencyManagement;
using OrderingSystem.Core.Infrastructure.TypeFinders;
using OrderingSystem.Data;
using OrderingSystem.Data.Repositories;
using OrderingSystem.IService;
using OrderingSystem.Service;
using System.Linq;
using System.Reflection;

namespace OrderingSystem.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
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
 
            // 注入Business及接口
            builder.RegisterAssemblyTypes(typeof(UserBusiness).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
        }
    }
}