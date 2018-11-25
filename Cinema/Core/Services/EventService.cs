using Core.Context;
using Core.Interfaces;
using Core.Models;
using System;

namespace Core.Services
{
    public class EventService : BaseService<Event, Guid>, IEventService
    {
        public EventService(CinemaContext context) : base(context)
        {
        }
    }
}
