using System;

namespace MedicalAppointmentApp.Models
{
    public class BlackedListedUser
    {
        public string UserId { get; set; }
        public bool IsBlackListed { get; set; }
        public DateTime? BlackListedEndDate { get; set; }
    }
}
