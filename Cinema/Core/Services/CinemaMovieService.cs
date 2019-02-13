using Core.Context;
using Core.Interfaces;
using Core.Models;
using Core.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CinemaMovieService : BaseEntityStateService<CinemaMovie>, ICinemaMovieService
    {
        protected readonly CinemaContext context;

        public CinemaMovieService(CinemaContext context)
            : base(context) => this.context = context;

        public async Task<List<CinemaMovie>> GetAllByMovieIdAsync(Guid id)
        {
            return await context
               .CinemaMovies
               .AsNoTracking()
               .Where(movie => movie.MovieId == id)
               .ToListAsync();
        }

        public async Task Delete(CinemaMovie entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task Delete(List<CinemaMovie> list)
        {
            context.CinemaMovies.RemoveRange(list);
            //context.Entry(list).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
