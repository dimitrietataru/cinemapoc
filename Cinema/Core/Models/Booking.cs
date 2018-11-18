using Core.Models.Base;
using Core.Models.Enums;
using System;

namespace Core.Models
{
    public class Booking : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public Guid EventId { get; set; }

        public BookingStatus Status { get; set; }
        public string Seat { get; set; }

        public virtual Event Event { get; set; }
    }
}
