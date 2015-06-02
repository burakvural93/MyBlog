using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcBurakBlog.Startup))]
namespace MvcBurakBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
