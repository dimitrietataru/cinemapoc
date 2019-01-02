using AutoMapper;
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

        public CinemaController(
            ICinemaService cinemaService,
            IMapper mapper)
        {
            this.cinemaService = cinemaService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cinemas = await cinemaService.GetAllAsync();
                var result = mapper.Map<List<CinemaIndexViewModel>>(cinemas);

                return View(result);
            }
            catch
            {
                // TODO: Add proper error page and Log
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

                var result = mapper.Map<CinemaDetailsViewModel>(cinema);

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
            return View();
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

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var cinema = await cinemaService.GetByIdAsync(id);
            var result = mapper.Map<CinemaEditViewModel>(cinema);

            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CinemaEditViewModel dto)
        {
            try
            {
                var cinema = mapper.Map<Cinema>(dto);
                await cinemaService.UpdateAsync(cinema);

                return RedirectToAction(nameof(Details), new { id = cinema.Id });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var cinema = await cinemaService.GetByIdAsync(id);
            var result = mapper.Map<CinemaDeleteViewModel>(cinema);

            return View(result);
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
                return View();
            }
        }
    }
}
