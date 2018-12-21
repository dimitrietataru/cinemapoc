using System;

namespace CMS.Models.Cinema
{
    public class CinemaIndexViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
