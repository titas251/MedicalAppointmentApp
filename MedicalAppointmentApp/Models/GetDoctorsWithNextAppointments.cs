using DAL.Data.Models;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentApp.Models
{
    public class GetDoctorsWithNextAppointments
    {
        public Doctor Doctor { get; set; }
        public ICollection<DateTime?> NextFreeAppointmentDates { get; set; }
    }
}
