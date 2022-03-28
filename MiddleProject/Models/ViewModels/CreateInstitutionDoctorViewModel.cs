using System;
using System.Collections.Generic;

namespace MiddleProject.Models.ViewModels
{
    public class CreateInstitutionDoctorViewModel
    {
        public int DoctorId { get; set; }
        public int InstitutionId { get; set; }
        public List<GetInstitutionModel> Institutions { get; set; }
        public List<ScheduleDetailModel> scheduleDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
