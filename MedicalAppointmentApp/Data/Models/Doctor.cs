using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Schedules = new HashSet<Schedule>();
            this.Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? NextFreeAppointmentDate { get; set; }
        public int MedicalSpecialityId { get; set; }
        public virtual MedicalSpeciality MedicalSpeciality { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
