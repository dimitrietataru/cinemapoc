using Core.Models.Base;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Cinema : StableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Schedule { get; set; }

        public virtual List<Auditorium> Auditoriums { get; set; } = new List<Auditorium>();
        public virtual List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
