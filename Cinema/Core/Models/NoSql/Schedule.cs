using System;

namespace Core.Models.NoSql
{
    public class Schedule
    {
        public TimeSpan Open { get; set; }
        public TimeSpan Close { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
