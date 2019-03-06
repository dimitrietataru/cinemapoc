using System.Collections.Generic;

namespace CMS.Models.CinemaMovie
{
    public class CinemaMovieViewModel
    {
        public CinemaMovieViewModel()
        {
        }

        public CinemaMovieViewModel(MovieAssignViewModel movie, List<CinemaAssignViewModel> cinemas)
        {
            Movie = movie;
            Cinemas = cinemas;
        }

        public MovieAssignViewModel Movie { get; set; }
        public List<CinemaAssignViewModel> Cinemas { get; set; } = new List<CinemaAssignViewModel>();
    }
}
