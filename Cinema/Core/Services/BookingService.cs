using Core.Context;
using Core.Interfaces;
using Core.Models;
using System;

namespace Core.Services
{
    public class BookingService : BaseService<Booking, Guid>, IBookingService
    {
        public BookingService(CinemaContext context) : base(context)
        {
        }
    }
}
