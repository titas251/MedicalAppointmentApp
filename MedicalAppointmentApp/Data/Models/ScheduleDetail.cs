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
        public string StartDateTime { get; set; }
        [Required]
        public string EndDateTime { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
