using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{ 
    public class ScheduleDetail
    { 
        [Key]
        public int ScheduleDetailId { get; set; }
        [Required]
        public DayOfWeek Day { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
