using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models.Auditorium.Partial;
using Core.Interfaces;

namespace CMS.ViewComponents
{
	public class AuditoriumViewComponent : ViewComponent
	{
		private readonly ICinemaService cinemaService;

		public AuditoriumViewComponent(ICinemaService cinemaService)
		{
			this.cinemaService = cinemaService;
		}

		public async Task<IViewComponentResult> InvokeAsync(int rows, int columns, Guid? cinemaId)
		{
			if (cinemaId.HasValue)
			{
				var cinema = await cinemaService.GetEagerByIdAsync(cinemaId.Value);
			}

			var dto = new AuditoriumSeatsViewModel(rows, columns);

			return View("_Seats", dto);
		}
	}
}
