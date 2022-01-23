namespace Domain.ConfigModels
{
    public class SlotConfig
    {
        public int StartSlotHour { get; set; }
        public int StartSlotMinute { get; set; }
        public int SlotDurationInMinutes { get; set; }
        public int EndSlotHour { get; set; }
        public int EndSlotMinute { get; set; }
    }
}
