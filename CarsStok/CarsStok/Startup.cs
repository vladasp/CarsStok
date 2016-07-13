using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarsStok.Startup))]
namespace CarsStok
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
