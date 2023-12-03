using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_4BroShop.Startup))]
namespace _4BroShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
