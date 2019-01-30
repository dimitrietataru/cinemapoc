using Core.Models;
using System;

namespace Core.Interfaces
{
    public interface IMovieService : IBaseService<Movie, Guid>
    {
    }
}
