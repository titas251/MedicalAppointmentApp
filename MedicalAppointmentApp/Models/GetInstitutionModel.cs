﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class GetInstitutionModel
    {
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<GetDoctorModel> Doctors { get; set; }
    }
}