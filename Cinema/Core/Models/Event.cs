using Core.Models.Base;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Event : BaseEntity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid AuditoriumId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Auditorium Auditorium { get; set; }
        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
