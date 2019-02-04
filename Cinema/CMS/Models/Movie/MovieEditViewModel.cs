using Core.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CoreModels = Core.Models;

namespace CMS.Models.Movie
{
    public class MovieEditViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DisplayName("Cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public Guid? CinemaId { get; set; }

        [DisplayName("Duration")]
        [DataType(DataType.Duration)]
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
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        public List<SelectListItem> CinemasList { get; set; } = new List<SelectListItem>();

        public void SetCinemas(List<CoreModels.Cinema> cinemas) 
        {
            cinemas.ForEach(cinema =>
            {
                CinemasList.Add(
                    new SelectListItem
                    {
                        Text = cinema.Name,
                        Value = cinema.Id.ToString()
                    });
            });
        }
    }
}