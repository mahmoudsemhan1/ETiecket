using eTickets.date;
using eTickets.date.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

        public async Task<IActionResult> Index()
        {
            var AllCinemas = await _cinemasService.GetAllAsync();
            return View(AllCinemas);
        }
        //get:Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _cinemasService.AddAsync(cinema);
            return RedirectToAction(nameof(Index));

        }

        //get : cinema/details
        public  async Task<IActionResult> Details(int id)
        {
            var CinemaDetalis = await _cinemasService.GetByIdAsync(id);
            if (CinemaDetalis == null) return View("NotFound");

            return View(CinemaDetalis);
        }
        //get :Cinema/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var CinemaDetalis =await  _cinemasService.GetByIdAsync(id);
            if (CinemaDetalis == null) return View("NotFound");

            return View(CinemaDetalis);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CinemaDetalis = await _cinemasService.GetByIdAsync(id);
            if (CinemaDetalis == null) return View("NotFound");

            await _cinemasService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //get :Cinema/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var CinemaDetalis = await _cinemasService.GetByIdAsync(id);
            if (CinemaDetalis == null) return View("NotFound");

            return View(CinemaDetalis);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Cinema cinema)
        {
            if (!ModelState.IsValid) return View("NotFound");

            await _cinemasService.UpdateAsync(id,cinema);
            return RedirectToAction(nameof(Index));

        }

    }
}

