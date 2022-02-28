using MedicalAppointmentApp.Data.Models;
using System;

namespace MedicalAppointmentApp.Models
{
    public class GetAppointmentModel
    {
        public int AppointmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Detail { get; set; }
        public GetDoctorModel Doctor { get; set; }
    }
}
