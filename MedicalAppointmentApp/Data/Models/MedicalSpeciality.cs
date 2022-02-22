using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class MedicalSpeciality
    {
        [Key]
        public int MedicalSpecialityId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Doctor> Doctors { get; set; }

    }
}
