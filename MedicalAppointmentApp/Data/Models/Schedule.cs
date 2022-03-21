using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Schedule
    {
        public Schedule()
        {
            this.ScheduleDetails = new HashSet<ScheduleDetail>();
        }

        [Key]
        public int ScheduleId { get; set; }
        public int InstitutionId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public Institution Institution { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; }
    }
}
