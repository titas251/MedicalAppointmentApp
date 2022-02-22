using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class GetMedicalSpecialtyModel
    {
        public int MedicalSpecialityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetDoctorModel> Doctors { get; set; }
    }
}
