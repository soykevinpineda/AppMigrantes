using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Migrantes.Areas.Identity.IdentityHostingStartup))]
namespace Migrantes.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}