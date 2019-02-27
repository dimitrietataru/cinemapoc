using System;
using System.ComponentModel;

namespace CMS.Models.Movie
{
    public class MovieDeleteViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
