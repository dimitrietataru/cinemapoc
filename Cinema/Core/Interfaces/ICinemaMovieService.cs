using Core.Interfaces.Base;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICinemaMovieService : IBaseEntityStateService<CinemaMovie>
    {
        Task<List<CinemaMovie>> GetAllByMovieIdAsync(Guid id);
        Task Delete(CinemaMovie entity);
        Task Delete(List<CinemaMovie> list);
    }
}
