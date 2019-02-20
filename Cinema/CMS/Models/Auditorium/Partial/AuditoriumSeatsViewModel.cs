using Core.Models.NoSql;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CMS.Models.Auditorium.Partial
{
	public class AuditoriumSeatsViewModel
	{
		public AuditoriumSeatsViewModel()
		{
		}

		public AuditoriumSeatsViewModel(int rows, int columns)
		{
			Rows = Math.Max(rows, 20);
			Columns = Math.Max(columns, 20);
			Seats = new List<Seat>(rows * columns)
			{
				new Seat(0,0)
			};
		}
		public int Rows { get; set; }
		public int Columns { get; set; }

		public List<Seat> Seats { get; set; } = new List<Seat>();
	}
}
