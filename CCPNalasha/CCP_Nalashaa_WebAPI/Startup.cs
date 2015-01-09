using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CCP_Nalashaa_WebAPI.Startup))]
namespace CCP_Nalashaa_WebAPI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
