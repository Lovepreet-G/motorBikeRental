using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(motorBikeRental.Startup))]
namespace motorBikeRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
