using System;

namespace Core.Models.NoSql
{
    public class Schedule
    {
        public Schedule()
        {
        }

        public Schedule(int day)
        {
            Open = new TimeSpan(9, 0, 0);
            Close = new TimeSpan(23, 0, 0);
            Day = (DayOfWeek)(day);
        }

        public TimeSpan Open { get; set; }
        public TimeSpan Close { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
