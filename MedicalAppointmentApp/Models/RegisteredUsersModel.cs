using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class RegisteredUsersModel
    {
        public List<UserWithRoleModel> Users { get; set; }
    }
}
