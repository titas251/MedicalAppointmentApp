using System;

namespace MedicalAppointmentApp.Models
{
    public class GetAppointmentModel
    {
        public GetUserModel ApplicationUser { get; set; }
        public int AppointmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Address { get; set; }
        public string Detail { get; set; }
        public GetDoctorModel Doctor { get; set; }
    }
}
