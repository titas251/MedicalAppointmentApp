using System;

namespace MiddleProject.Models
{
    public class ScheduleDetailModel
    {
        public int ScheduleDetailId { get; set; }
        public DayOfWeek Day { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public int ScheduleId { get; set; }
        public bool isWorking { get; set; }
    }
}
