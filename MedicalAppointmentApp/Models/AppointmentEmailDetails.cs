using System;

namespace MedicalAppointmentApp.Models
{
    public class AppointmentEmailDetails
    {
        public string Email { get; set; }
        public DateTime AppointmentStartDateTime { get; set; }
        public string Detail { get; set; }
        public string Address { get; set; }
    }
}
