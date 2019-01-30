using Core.Models.Enums;
using System;

namespace CMS.Models.Movie
{
    public class IndexViewModel
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public ProjectionType ProjectionType { get; set; }
        public string Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
