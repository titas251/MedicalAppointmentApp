using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Institution
    {
        [Key]
        public int InstitutionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual List<InstitutionDoctor> Doctors { get; set; }

    }
}
