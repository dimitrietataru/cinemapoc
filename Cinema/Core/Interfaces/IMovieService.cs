using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMovieService : IBaseService<Movie, Guid>
    {
        Task<List<Movie>> GetPagedAsync(int page, string orderBy, bool orderDesc, string matchString = null);
    }
}
