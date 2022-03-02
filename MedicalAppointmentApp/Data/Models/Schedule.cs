using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
