using System;

namespace Domain.ViewModels
{
    public class TimeSlotModel
    {
        public DateTime StartTime { get; set; }
        public string StartTimeDisplay => StartTime.ToString("hh:mm tt");
    }
}
