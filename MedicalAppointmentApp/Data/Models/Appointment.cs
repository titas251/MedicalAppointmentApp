using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        public string Detail { get; set; }
        [Required]
        public string Address { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
