using System;

namespace LagosArch.Models
{
    public class Confession
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public string StartTimeString { get; set; }
        public DateTime EndTime { get; set; }
        public string EndTimeString { get; set; }
        public int ParishId { get; set; }
    }
}
