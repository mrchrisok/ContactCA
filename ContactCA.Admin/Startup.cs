using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactCA.Admin.Startup))]
namespace ContactCA.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
