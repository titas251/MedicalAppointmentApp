using BackgroundJobs.EmailSending;
using BackgroundJobs.UpdateAppointment;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SendGrid.Extensions.DependencyInjection;
using System;

namespace BackgroundJobs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging();
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    hostContext.Configuration.GetConnectionString("DefaultConnection")));
                services.AddSendGrid(options =>
                {
                    options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                });

                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    q.ScheduleJob<SendMailJob>(trigger => trigger
                    .WithIdentity("SendRecurringMailTrigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(18, 0))
                    .WithDescription("This trigger will run every day at 18:00.")
                    );
                    q.ScheduleJob<UpdateNextFreeAppointments>(trigger => trigger
                    .WithIdentity("RecurringUpdateNextFreeAppointmentsTrigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 01))
                    .WithDescription("This trigger will run every day at 00:01.")
                    );
                });

                services.AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = true;
                });
            });
    }
}
