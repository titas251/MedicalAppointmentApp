using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MedicalAppointmentApp.Areas.Identity.IdentityHostingStartup))]
namespace MedicalAppointmentApp.Areas.Identity
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