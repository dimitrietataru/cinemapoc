using Core.Models.Base;
using Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Auditorium : StableEntity<Guid>
    {
        public Guid CinemaId { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Seats { get; set; }
        public AuditoriumStatus Status { get; set; }
        public AuditoriumSeatType SeatType { get; set; }
        public AuditoriumScreenType ScreenType { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual List<Event> Events { get; set; } = new List<Event>();
    }
}
