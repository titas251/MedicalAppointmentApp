﻿using MedicalAppointmentApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class GetDoctorModel
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int MedicalSpecialityId { get; set; }
        public MedicalSpeciality MedicalSpeciality { get; set; }
        public ICollection<Institution> Institutions { get; set; }
    }
}
