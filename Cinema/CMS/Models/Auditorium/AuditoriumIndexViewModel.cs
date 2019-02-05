using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Auditorium
{
	public class AuditoriumIndexViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }
		public int Capacity { get; set; }
		public AuditoriumStatus Status { get; set; }
		public AuditoriumSeatType SeatType { get; set; }
		public AuditoriumScreenType ScreenType { get; set; }
	}
}
