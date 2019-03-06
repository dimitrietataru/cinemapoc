using Core.Interfaces.Base;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICinemaMovieService : IBaseEntityService<CinemaMovie>
    {
        Task<List<CinemaMovie>> GetByMovieIdAsync(Guid movieId);
        Task SyncAsync(Guid movieId, List<KeyValuePair<Guid, bool>> cinemas);
    }
}
