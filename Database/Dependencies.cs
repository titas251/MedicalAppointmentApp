using DAL.Data;
using DAL.Data.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterDbContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //register repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IMedicalSpecialityRepository, MedicalSpecialityRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IScheduleDetailRepository, ScheduleDetailRepository>();

            return services;
        }
    }
}
