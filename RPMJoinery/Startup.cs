using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPMJoinery.Startup))]
namespace RPMJoinery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
