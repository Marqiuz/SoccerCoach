using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SoccerCoach.Web.Areas.Identity.IdentityHostingStartup))]

namespace SoccerCoach.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}