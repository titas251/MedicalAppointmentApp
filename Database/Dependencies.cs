using DAL.Data;
using DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace DAL
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterDbContext(
            this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                   "Data Source = LAPTOP - ICDL3632\\MEDICALDB; Initial Catalog = medicalappdb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"));

            //var con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var con = "Data Source = LAPTOP - ICDL3632\\MEDICALDB; Initial Catalog = medicalappdb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";


            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
