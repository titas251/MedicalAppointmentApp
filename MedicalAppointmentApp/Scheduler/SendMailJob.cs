using Microsoft.Extensions.Logging;
using Quartz;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Scheduler
{
    public class SendMailJob : IJob
    {
        private readonly ILogger logger;
        private readonly ISendGridClient sendGridClient;

        public SendMailJob(ISendGridClient sendGridClient, ILogger<SendMailJob> logger)
        {
            this.sendGridClient = sendGridClient;
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("medicalapp255@gmail.com", "MedicalApp"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, especially with C# .NET"
            };
            msg.AddTo(new EmailAddress("krtitas1@gmail.com", "Client"));
            var response = await sendGridClient.SendEmailAsync(msg);

            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            logger.LogInformation(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
        }
    }
}
