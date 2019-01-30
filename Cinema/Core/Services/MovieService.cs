using Core.Context;
using Core.Interfaces;
using Core.Models;
using System;

namespace Core.Services
{
    public class MovieService : BaseService<Movie, Guid>, IMovieService
    {
        public MovieService(CinemaContext context) : base(context)
        {
        }
    }
}
