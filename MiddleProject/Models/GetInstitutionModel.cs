using DAL.Data.Models;
using System.Collections.Generic;

namespace MiddleProject.Models
{
    public class GetInstitutionModel
    {
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
