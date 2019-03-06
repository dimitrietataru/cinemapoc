using Core.Models.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models.Movie
{
    public class MovieCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [DisplayName("Duration (min)")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Actors is required")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Actors")]
        public string Actors { get; set; }

        [Required(ErrorMessage = "Studio is required")]
        [MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
        [DisplayName("Studio")]
        public string Studio { get; set; }

        [Required(ErrorMessage = "Projection type is required")]
        [DisplayName("Projection type")]
        public ProjectionType ProjectionType { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [DisplayName("Rating")]
        public MovieRating Rating { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("Genre")]
        public string Genre { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("Trailer Url")]
        public string TrailerUrl { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("Poster Url")]
        public string PosterUrl { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("IMDb Url")]
        public string ImbdUrl { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }
    }
}
