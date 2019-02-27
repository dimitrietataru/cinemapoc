using AutoMapper;
using CMS.Models;
using CMS.Models.Cinema;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;
        private readonly IMapper mapper;

        public CinemaController(ICinemaService cinemaService, IMapper mapper)
        {
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
                var pagedQuery = cinemaService.GetPagedQuery(orderBy, isAsc, filter, isExact);
                var cinemas = await cinemaService.GetPagedAsync(pagedQuery, page - 1, size);
                var count = await cinemaService.GetPagedCountAsync(pagedQuery);

                var viewItems = mapper.Map<List<CinemaIndexViewModel>>(cinemas);
                var dto = new PagedViewModel<CinemaIndexViewModel>(
                    viewItems, page, size, orderBy, isAsc, filter, isExact, count);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home"); 
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(id);
                if (cinema is null)
                {
                    return NotFound();
                }

                var dto = mapper.Map<CinemaDetailsViewModel>(cinema);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Create()
        {
            var dto = new CinemaCreateViewModel();

            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemaCreateViewModel dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var cinema = mapper.Map<Cinema>(dto);
                await cinemaService.CreateAsync(cinema);

                return RedirectToAction(nameof(Details), new { id = cinema.Id });
            }
            catch
            {
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(id);
                var dto = mapper.Map<CinemaEditViewModel>(cinema);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CinemaEditViewModel dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var cinema = mapper.Map<Cinema>(dto);
                await cinemaService.UpdateAsync(cinema);

                return RedirectToAction(nameof(Details), new { id = cinema.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(id);
                var dto = mapper.Map<CinemaDeleteViewModel>(cinema);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CinemaDeleteViewModel dto)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(dto.Id);
                await cinemaService.DeleteAsync(cinema);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
