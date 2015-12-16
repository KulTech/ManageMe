using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManageMe.Startup))]
namespace ManageMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
