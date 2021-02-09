using Autofac;
using OrderingSystem.Core.Infrastructure.TypeFinders;

namespace OrderingSystem.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
