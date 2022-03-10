using System;

namespace MedicalAppointmentApp.Models
{
    public class ClosestDateWithWorkingTime
    {
        public DateTime ClosestDate { get; set; }
        public string StartingDateTime { get; set; }
        public string EndingDateTime { get; set; }
    }
}
