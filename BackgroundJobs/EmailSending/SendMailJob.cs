using DAL.Data;
using DAL.Data;
using Microsoft.Extensions.Logging;
using Quartz;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace BackgroundJobs.EmailSending
{
    public class SendMailJob : IJob
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SendMailJob> _logger;

        public SendMailJob(ApplicationDbContext context, ISendGridClient sendGridClient, ILogger<SendMailJob> logger)
        {
            _sendGridClient = sendGridClient;
            _context = context;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            //Console.WriteLine("HelloJob is executing.");

            var emailsDetails = await UpcomingAppointments.GetEmailsToSend(_context);
            var senderEmail = new EmailAddress("medicalapp255@gmail.com", "MedicalApp");

            foreach (var emailDetails in emailsDetails)
            {
                if (emailDetails.AppointmentStartDateTime <= DateTime.Now.AddDays(1))
                {
                    var message = new SendGridMessage()
                    {
                        From = senderEmail,
                        Subject = "Medical appointment reminder",
                        PlainTextContent = "Your appointment date: " + emailDetails.AppointmentStartDateTime
                            + ". Location:  " + emailDetails.Address + ". Details: " + emailDetails.Detail
                    };

                    message.AddTo(new EmailAddress(emailDetails.Email, "Patient"));
                    var response = await _sendGridClient.SendEmailAsync(message);

                    // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
                    _logger.LogInformation(response.IsSuccessStatusCode ? "Email to " + emailDetails.Email
                        + " queued successfully!" : "Something went wrong!");
                }
            }
        }
    }
}
