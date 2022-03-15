using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Appointments = new HashSet<Appointment>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlackListed { get; set; }
        public DateTime? BlackListedEndDate { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
