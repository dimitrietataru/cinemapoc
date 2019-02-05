using System;

namespace CMS.Models.Movie
{
    public class MovieIndexViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Duration { get; set; }
        public string Studio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
