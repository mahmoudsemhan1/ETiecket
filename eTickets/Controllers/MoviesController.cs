using eTickets.date;
using eTickets.date.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesServices _moviesServices;

        public MoviesController( IMoviesServices moviesServices)
        {
            
            _moviesServices = moviesServices;
        }

        public async Task<IActionResult> Index()
        {
            var AllMovies= await _moviesServices.GetAllAsync(n=>n.Cinema);
            return View(AllMovies);
        }
        public async Task<IActionResult> Filter(string SearchString)
        {
            var AllMovies = await _moviesServices.GetAllAsync(n => n.Cinema);
            if (!string.IsNullOrEmpty(SearchString))
            {
                var FilteredResult = AllMovies.Where(n => n.Name.Contains(SearchString) || n.Description.Contains(SearchString)).ToList();
                return View("Index",FilteredResult);
            }
            return View("Index",AllMovies);
        }
        //Get:Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {

            var MovieDetails= await _moviesServices.GetMovieByIdAsync(id);
            return View(MovieDetails);
        }
        //get 
        public async  Task<IActionResult> Create()
        {
            var DropdownData = await _moviesServices.GetNewMovieDropdownvalue();

            ViewBag.Cinemas = new SelectList(DropdownData.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(DropdownData.producers, "Id", "FullName");
            ViewBag.Actors= new SelectList(DropdownData.Actors, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieMV movie)
        {
            if (!ModelState.IsValid)
            {
                var DropdownData = await _moviesServices.GetNewMovieDropdownvalue();

                ViewBag.Cinemas = new SelectList(DropdownData.cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(DropdownData.producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(DropdownData.Actors, "Id", "FullName");
                return View(movie);
            }

            await _moviesServices.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));

        }

        //get :Movie/Edite
        public async Task<IActionResult> Edit(int id)
        {
            var MovieDetails = await _moviesServices.GetMovieByIdAsync(id);
            if (MovieDetails == null) return View("NotFound");
            var response = new NewMovieMV()
            {
                Id=MovieDetails.Id,
                Name=MovieDetails.Name,
                Description=MovieDetails.Description,
                Price=MovieDetails.Price,
                StartDate=MovieDetails.StartDate,
                EndDate=MovieDetails.EndDate,
                ImageURL=MovieDetails.ImageURL,
                MovieCategory=MovieDetails.MovieCategory,
                CinemaId=MovieDetails.CinemaId,
                producerId=MovieDetails.producerId,
                ActorIds=MovieDetails.Actor_Movies.Select(n=>n.ActorId).ToList(),
            };

            var DropdownData = await _moviesServices.GetNewMovieDropdownvalue();
            ViewBag.Cinemas = new SelectList(DropdownData.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(DropdownData.producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(DropdownData.Actors, "Id", "FullName");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( int id,NewMovieMV movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var DropdownData = await _moviesServices.GetNewMovieDropdownvalue();

                ViewBag.Cinemas = new SelectList(DropdownData.cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(DropdownData.producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(DropdownData.Actors, "Id", "FullName");
                return View(movie);
            }

            await _moviesServices.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));

        }

    }
}
