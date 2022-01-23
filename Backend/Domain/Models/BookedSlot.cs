namespace Domain.Models
{
    public class BookedSlot : BaseModel
    {
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
    }
}
