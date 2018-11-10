using System;

namespace Core.Models
{
    public class CinemaMovie
    {
        public Guid CinemaId { get; set; }
        public Guid MovieId { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
