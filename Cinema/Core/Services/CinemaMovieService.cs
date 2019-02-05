using System.Threading.Tasks;
using Core.Context;
using Core.Interfaces;
using Core.Models;
using Core.Services.Base;

namespace Core.Services
{
    public class CinemaMovieService : BaseEntityStateService<CinemaMovie>, ICinemaMovieService
    {
        protected readonly CinemaContext context;

        public CinemaMovieService(CinemaContext context)
            : base(context) => this.context = context;
    }
}
