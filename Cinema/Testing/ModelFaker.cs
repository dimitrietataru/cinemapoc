using CMS.Models.Cinema;
using Core.Models;
using Core.Models.NoSql;
using System.Collections.Generic;
using System.Linq;
using Core.Models.Enums;
using System;
using CMS.Models.Movie;

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

        public Movie GetTestMovie()
        {
            return new Movie
            {
                Id = default,
                Name = default,
                Description = default,
                Duration = default,
                Actors = default,
                Studio = default,
                ProjectionType = ProjectionType.D2D,
                Rating = MovieRating.G,
                Genre = default,
                TrailerUrl = default,
                PosterUrl = default,
                ImbdUrl = default,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
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

        public MovieIndexViewModel GetTestMovieIndex()
        {
            var movies = GetTestMovies(10);

            return new MovieIndexViewModel(movies, 1, 10, 10)
            {
                OrderBy = default,
                OrderDesc = default
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

        public List<Movie> GetTestMovies(int count)
        {
            return Enumerable.Repeat(GetTestMovie(), count).ToList();
        }

        public List<CinemaIndexViewModel> GetTestCinemaIndexes(int count)
        {
            return Enumerable.Repeat(GetTestCinemaIndex(), count).ToList();
        }

        public List<MovieIndexViewModel> GetTestMovieIndexes(int count)
        {
            return Enumerable.Repeat(GetTestMovieIndex(), count).ToList();
        }
    }
}
