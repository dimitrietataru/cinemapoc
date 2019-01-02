using CMS.Models.Cinema;
using Core.Models;
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
                Schedule = string.Empty
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
                Schedule = string.Empty
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
                Schedule = string.Empty
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
    }
}
