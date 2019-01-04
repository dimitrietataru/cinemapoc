using Core.Models.NoSql;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Linq.Enumerable;

namespace CMS.Models.Cinema
{
    public class CinemaCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1000, ErrorMessage = "Maximum length exceeded (1000 characters)")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
        [DisplayName("Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        [MaxLength(200, ErrorMessage = "Maximum length exceeded (200 characters)")]
        [DisplayName("Contact")]
        public string Contact { get; set; }

        public List<Schedule> Schedules { get; set; } = Range(0, 7).Select(d => new Schedule(d)).ToList();
    }
}
