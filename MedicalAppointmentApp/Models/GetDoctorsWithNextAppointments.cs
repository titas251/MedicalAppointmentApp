using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Mediator.Queries;
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
