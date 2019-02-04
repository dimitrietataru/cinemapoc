using CMS.Models.Cinema;
using CMS.Models.Movie;
using Core.Models;
using Core.Models.Enums;
using Core.Models.NoSql;
using System.Collections.Generic;
using System.Linq;

namespace Testing
{
    public class ModelFaker
    {
        public Cinema GetTestCinema()
        {
            return new Cinema
            {
                Id = default,
                Name = string.Empty,
                Description = string.Empty,
                Location = string.Empty,
                Address = string.Empty,
                Contact = string.Empty,
                Schedule = string.Empty,
                CreatedBy = default,
                UpdatedAt = default,
                UpdatedBy = default,
                IsDeleted = default
            };
        }
        
        public CinemaIndexViewModel GetTestCinemaIndex()
        {
            return new CinemaIndexViewModel
            {
                Id = default,
                Name = string.Empty,
                Location = string.Empty,
                Address = string.Empty,
                Contact = string.Empty
            };
        }

        public CinemaDetailsViewModel GetTestCinemaDetails()
        {
            return new CinemaDetailsViewModel
            {
                Name = string.Empty,
                Description = string.Empty,
                Location = string.Empty,
                Address = string.Empty,
                Contact = string.Empty,
                Schedules = new List<Schedule>()
            };
        }

        public CinemaCreateViewModel GetTestCinemaCreate()
        {
            return new CinemaCreateViewModel
            {
                Name = string.Empty,
                Description = string.Empty,
                Location = string.Empty,
                Address = string.Empty,
                Contact = string.Empty,
                Schedules = new List<Schedule>()
            };
        }

        public CinemaEditViewModel GetTestCinemaEdit()
        {
            return new CinemaEditViewModel
            {
                Id = default,
                Name = string.Empty,
                Description = string.Empty,
                Location = string.Empty,
                Address = string.Empty,
                Contact = string.Empty,
                Schedules = new List<Schedule>()
            };
        }

        public CinemaDeleteViewModel GetTestCinemaDelete()
        {
            return new CinemaDeleteViewModel
            {
                Id = default,
                Name = string.Empty,
                Description = string.Empty
            };
        }

        public List<Cinema> GetTestCinemas(int count)
        {
            return Enumerable.Repeat(GetTestCinema(), count).ToList();
        }

        public List<CinemaIndexViewModel> GetTestCinemaIndexes(int count)
        {
            return Enumerable.Repeat(GetTestCinemaIndex(), count).ToList();
        }

        public Movie GetTestMovie()
        {
            return new Movie
            {
                Id = default,
                Name = string.Empty,
                Description = string.Empty,
                Duration = default,
                Actors = default,
                Studio = default,
                ProjectionType = ProjectionType.D2D,
                Rating = MovieRating.G,
                Genre = default,
                TrailerUrl = default,
                PosterUrl = default,
                ImbdUrl = default,
                StartDate = default,
                EndDate = default,
                CreatedBy = default,
                UpdatedAt = default,
                UpdatedBy = default,
                IsDeleted = default
            };
        }

        public MovieIndexViewModel GetTestMovieIndex()
        {
            return new MovieIndexViewModel
            {
                Id = default,
                Name = string.Empty,
                Duration = default,
                Studio = default,
                StartDate = default,
                EndDate = default
            };
        }

        public List<Movie> GetTestMovies(int count)
        {
            return Enumerable.Repeat(GetTestMovie(), count).ToList();
        }

        public List<MovieIndexViewModel> GetTestMovieIndexes(int count)
        {
            return Enumerable.Repeat(GetTestMovieIndex(), count).ToList();
        }
    }
}
