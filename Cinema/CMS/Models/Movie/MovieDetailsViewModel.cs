using Core.Models;
using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models.Movie
{
    public class MovieDetailsViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Duration")]
        public int Duration { get; set; }

        [DisplayName("Actors")]
        public string Actors { get; set; }

        [DisplayName("Studio")]
        public string Studio { get; set; }

        [DisplayName("ProjectionType")]
        public ProjectionType ProjectionType { get; set; }

        [DisplayName("Rating")]
        [Required(ErrorMessage = "Rating is required")]
        public MovieRating Rating { get; set; }

        [DisplayName("Genre")]
        public string Genre { get; set; }

        [DisplayName("Trailer Url")]
        public string TrailerUrl { get; set; }

        [DisplayName("Poster Url")]
        public string PosterUrl { get; set; }

        [DisplayName("Imbd Url")]
        public string ImbdUrl { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}