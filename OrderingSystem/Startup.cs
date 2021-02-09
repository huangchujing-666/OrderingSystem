using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderingSystem.Startup))]
namespace OrderingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
