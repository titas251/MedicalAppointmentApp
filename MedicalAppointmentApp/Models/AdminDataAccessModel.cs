using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class AdminDataAccessModel
    {
        public readonly UserManager<IdentityUser> userManager;

        public AdminDataAccessModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

    }
}
