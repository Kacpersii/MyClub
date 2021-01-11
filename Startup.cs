using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyClub.Startup))]
namespace MyClub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
