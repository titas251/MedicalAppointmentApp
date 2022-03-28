using DAL.Data.Models;
using System.Collections.Generic;

namespace MedicalAppointmentApp.Models
{
    public class GetMedicalSpecialtyModel
    {
        public int MedicalSpecialityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
