using Core.Models.Enums;
using System;
using System.ComponentModel;

namespace CMS.Models.Movie
{
    public class MovieDetailsViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Minutes")]
        public int Duration { get; set; }

        [DisplayName("Actors")]
        public string Actors { get; set; }

        [DisplayName("Studio")]
        public string Studio { get; set; }

        [DisplayName("Projection type")]
        public ProjectionType ProjectionType { get; set; }

        [DisplayName("Rating")]
        public MovieRating Rating { get; set; }

        [DisplayName("Genre")]
        public string Genre { get; set; }

        [DisplayName("Trailer Url")]
        public string TrailerUrl { get; set; }

        [DisplayName("Poster Url")]
        public string PosterUrl { get; set; }

        [DisplayName("IMDb Url")]
        public string ImbdUrl { get; set; }

        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        public DateTime EndDate { get; set; }
    }
}
