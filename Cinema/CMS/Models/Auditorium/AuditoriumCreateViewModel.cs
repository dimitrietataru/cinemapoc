using Core.Models.Enums;
using Core.Models.NoSql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static System.Linq.Enumerable;

namespace CMS.Models.Auditorium
{
	public class AuditoriumCreateViewModel
	{
		public Guid CinemaId { get; set; } = Guid.Parse("3659AAB8-4FD8-49BF-418A-08D685E94007");

		[Required(ErrorMessage = "Name is required")]
		[MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
		[DisplayName("Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Capacity is required")]
		[DisplayName("Capacity")]
		public int Capacity { get; set; }

		[Required(ErrorMessage = "Rows is required")]
		[DisplayName("Rows")]
		public int Rows { get; set; }

		[Required(ErrorMessage = "Columns is required")]
		[DisplayName("Columns")]
		public int Columns { get; set; }

		[DisplayName("Status")]
		public AuditoriumStatus Status { get; set; }

		[DisplayName("Seat Type")]
		public AuditoriumSeatType SeatType { get; set; }

		[DisplayName("Screen Type")]
		public AuditoriumScreenType ScreenType { get; set; }

		public List<Seat> Seats { get; set; } = new List<Seat>();
	}
}
