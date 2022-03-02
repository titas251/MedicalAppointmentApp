using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models.ViewModels
{
    public class CreateInstitutionDoctorViewModel
    {
        public int DoctorId { get; set; }
        public int InstitutionId { get; set; }
        public List<GetInstitutionModel> Institutions { get; set; }
        public List<ScheduleDetailModel> scheduleDetails { get; set; }
    }
}
