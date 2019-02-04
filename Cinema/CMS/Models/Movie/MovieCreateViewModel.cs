using Core.Models.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models.Movie
{
    public class MovieCreateViewModel
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DisplayName("Duration")]
        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        [DisplayName("Actors")]
        public string Actors { get; set; }

        [DisplayName("Studio")]
        public string Studio { get; set; }

        [DisplayName("ProjectionType")]
        [Required(ErrorMessage = "Projection Type is required")]
        public ProjectionType ProjectionType { get; set; }

        [DisplayName("Rating")]
        [Required(ErrorMessage = "Rating is required")]
        public MovieRating Rating { get; set; }

        [DisplayName("Genre")]
        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [DisplayName("Trailer Url")]
        public string TrailerUrl { get; set; }

        [DisplayName("Poster Url")]
        public string PosterUrl { get; set; }

        [DisplayName("Imbd Url")]
        public string ImbdUrl { get; set; }

        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
    }
}