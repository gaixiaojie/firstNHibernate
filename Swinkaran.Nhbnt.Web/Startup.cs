using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Swinkaran.Nhbnt.Web.Startup))]
namespace Swinkaran.Nhbnt.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
