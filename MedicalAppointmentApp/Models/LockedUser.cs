using System;

namespace MedicalAppointmentApp.Models
{
    public class LockedUser
    {
        public string UserId { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockoutEndDate { get; set; }
    }
}
