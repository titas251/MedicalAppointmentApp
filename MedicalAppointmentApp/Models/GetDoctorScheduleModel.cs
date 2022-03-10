using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Mediator.Queries;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentApp.Models
{
    public class GetDoctorScheduleModel
    {
        public Doctor Doctor { get; set; }
        public ClosestDateWithWorkingTime NextFreeAppointmentDate { get; set; } 
    }
}
