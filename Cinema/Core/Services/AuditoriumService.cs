using Core.Context;
using Core.Interfaces;
using Core.Models;
using System;

namespace Core.Services
{
    public class AuditoriumService : BaseService<Auditorium, Guid>, IAuditoriumService
    {
        public AuditoriumService(CinemaContext context) : base(context)
        {
        }
    }
}
