using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoNuvem.Startup))]
namespace ProjetoNuvem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
