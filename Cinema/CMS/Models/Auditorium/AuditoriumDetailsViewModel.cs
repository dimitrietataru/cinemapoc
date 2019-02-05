﻿using Core.Models.Enums;
using Core.Models.NoSql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Auditorium
{
	public class AuditoriumDetailsViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[MaxLength(50, ErrorMessage = "Maximum length exceeded (50 characters)")]
		[DisplayName("Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Capacity is required")]
		[DisplayName("Capacity")]
		public int Capacity { get; set; }

		[DisplayName("Status")]
		public AuditoriumStatus Status { get; set; }

		[DisplayName("Seat Type")]
		public AuditoriumSeatType SeatType { get; set; }

		[DisplayName("Screen Type")]
		public AuditoriumScreenType ScreenType { get; set; }

		public List<Seat> Seats { get; set; }
	}
}
