using Core.Models.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models.Movie
{
    public class MovieEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Maximum length exceeded (500 characters)")]
        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [DisplayName("Duration")]
        public int Duration { get; set; }

        [MaxLength(250, ErrorMessage = "Maximum length exceeded (250 characters)")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Actors")]
        public string Actors { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
        [DisplayName("Studio")]
        public string Studio { get; set; }

        [Required(ErrorMessage = "Projection Type is required")]
        [DisplayName("ProjectionType")]
        public ProjectionType ProjectionType { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [DisplayName("Rating")]
        public MovieRating Rating { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
        [Required(ErrorMessage = "Genre is required")]
        [DisplayName("Genre")]
        public string Genre { get; set; }

        [DisplayName("Trailer Url")]
        public string TrailerUrl { get; set; }

        [DisplayName("Poster Url")]
        public string PosterUrl { get; set; }

        [DisplayName("Imbd Url")]
        public string ImbdUrl { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }
    }
}