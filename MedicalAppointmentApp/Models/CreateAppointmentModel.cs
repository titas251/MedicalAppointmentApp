using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class CreateAppointmentModel
    {
        public int AppointmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Detail { get; set; }
        public string ApplicationUserId { get; set; }
        public int DoctorId { get; set; }
    }
}
