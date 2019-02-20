using AutoMapper;
using CMS.Models;
using CMS.Models.Auditorium;
using CMS.Models.Cinema;
using Core.Interfaces;
using Core.Models;
using Core.Models.NoSql;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Controllers
{
	public class AuditoriumController : Controller
	{
		private readonly IAuditoriumService auditoriumService;
		private readonly ICinemaService cinemaService;
		private readonly IMapper mapper;

		public AuditoriumController(
			IAuditoriumService auditoriumService,
			ICinemaService cinemaService,
			IMapper mapper)
		{
			this.auditoriumService = auditoriumService;
			this.cinemaService = cinemaService;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index(
			int page = 1,
			int size = 10,
			string orderBy = "Name",
			bool isAsc = true,
			string filter = "",
			bool isExact = false)
		{
			try
			{
				var pagedQuery = auditoriumService.GetPagedQuery(orderBy, isAsc, filter, isExact);
				var auditoriums = await auditoriumService.GetPagedAsync(pagedQuery, page - 1, size);
				var count = await auditoriumService.GetPagedCountAsync(pagedQuery);

				var viewItems = mapper.Map<List<AuditoriumIndexViewModel>>(auditoriums);
				var dto = new PagedViewModel<AuditoriumIndexViewModel>(
					viewItems, page, size, orderBy, isAsc, filter, isExact, count);

				return View(dto);
			}
			catch (Exception)
			{
				// TODO: Add proper error page and Log
				return RedirectToAction("Index", "Home");
			}
		}

		public async Task<IActionResult> Details(Guid id)
		{
			try
			{
				var auditorium = await auditoriumService.GetEagerByIdAsync(id);
				if (auditorium is null)
				{
					return NotFound();
				}

				var result = mapper.Map<AuditoriumDetailsViewModel>(auditorium);

				return View(result);
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
		}

#pragma warning disable CS1998
		public async Task<IActionResult> Create()
#pragma warning restore CS1998
		{
			var cinemas = await cinemaService.GetAllAsync();
			var cinemasDetail = mapper.Map<List<CinemaDetailsViewModel>>(cinemas);
			var dto = new AuditoriumCreateViewModel(cinemasDetail);
			dto.AuditoriumSeats.Seats.Add(new Seat(0, 0));

			return View(dto);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AuditoriumCreateViewModel dto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(dto);
				}

				//dto.Seats = GetSeats();
				var auditorium = mapper.Map<Auditorium>(dto);
				await auditoriumService.CreateAsync(auditorium);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				return View(dto);
			}
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			try
			{
				var auditorium = await auditoriumService.GetEagerByIdAsync(id);
				var result = mapper.Map<AuditoriumEditViewModel>(auditorium);

				return View(result);
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(AuditoriumEditViewModel dto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(dto);
				}

				var auditorium = mapper.Map<Auditorium>(dto);
				await auditoriumService.UpdateAsync(auditorium);

				return RedirectToAction(nameof(Details), new { id = auditorium.Id });
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public async Task<IActionResult> _Seats(int rows, int columns, Guid cinemaId)
		{
			return ViewComponent("Auditorium", new { rows, columns, cinemaId });
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var auditorium = await auditoriumService.GetByIdAsync(id);
				var result = mapper.Map<AuditoriumDeleteViewModel>(auditorium);

				return View(result);
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(AuditoriumDeleteViewModel dto)
		{
			try
			{
				var auditorium = await auditoriumService.GetByIdAsync(dto.Id);
				await auditoriumService.DeleteAsync(auditorium);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
		}

		private static List<Seat> GetSeats(int totalRows = 20, int totalColumns = 20)
		{
			var seats = new List<Seat>();

			for (int i = 0; i < totalRows; i++)
			{
				for (int j = 0; j < totalColumns; j++)
				{
					seats.Add(new Seat(i, j));
				}
			}

			return seats;
		}
	}
}
