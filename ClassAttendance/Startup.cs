using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassAttendance.Startup))]
namespace ClassAttendance
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
