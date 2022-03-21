using MedicalAppointmentApp.Data.Models;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentApp.Models
{
    public class CreateAppointmentModel
    {
        public int AppointmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Address { get; set; }
        public string Detail { get; set; }
        public string ApplicationUserId { get; set; }
        public int DoctorId { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public List<ScheduleDetail> DoctorScheduleDetails { get; set; }
        public List<GetAppointmentModel> DoctorAppointments { get; set; }
    }
}
