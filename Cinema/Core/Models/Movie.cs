using Core.Models.Base;
using Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Movie : StableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Actors { get; set; }
        public string Studio { get; set; }
        public ProjectionType ProjectionType { get; set; }
        public MovieRating Rating { get; set; }
        public string Genre { get; set; }
        public string TrailerUrl { get; set; }
        public string PosterUrl { get; set; }
        public string ImbdUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual List<Cinema> Cinemas { get; set; } = new List<Cinema>();
        public virtual List<Event> Events { get; set; } = new List<Event>();
    }
}
