using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(streaming_inż.Startup))]
namespace streaming_inż
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
