using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kyrsovik.Startup))]
namespace Kyrsovik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
