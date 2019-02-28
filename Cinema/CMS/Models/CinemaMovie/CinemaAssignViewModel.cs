using System;

namespace CMS.Models.CinemaMovie
{
    public class CinemaAssignViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }
}
