using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;

namespace BackgroundJobs.EmailSending
{
    public static class UpcomingAppointments
    {
        public static async Task<List<AppointmentEmailDetails>> GetEmailsToSend(ApplicationDbContext context)
        {
            var emails = new List<AppointmentEmailDetails>();

            var appointments = await context.Appointments.Include(appointment => appointment.ApplicationUser)
                .Where(appointment => appointment.StartDateTime >= DateTime.Now)
                .ToListAsync();

            foreach (var appointment in appointments)
            {
                var emailDetails = new AppointmentEmailDetails {
                    Address = appointment.Address,
                    Detail = appointment.Detail,
                    Email = appointment.ApplicationUser.Email,
                    AppointmentStartDateTime = appointment.StartDateTime
                };
                emails.Add(emailDetails);
            }

            return emails;
        }
    }
}
