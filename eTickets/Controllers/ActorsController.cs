using eTickets.date;
using eTickets.date.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActionsService _srvice;

        public ActorsController(IActionsService srvice)
        {
            _srvice = srvice;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _srvice.GetAllAsync());
        }
        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
    
     
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                // Model state is not valid, return a BadRequest with error messages
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var errorMessage = string.Join(" ", errorMessages);
                return BadRequest(errorMessage);
            }

            // Model state is valid, proceed with adding the actor
            await _srvice.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //get :Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var ActorDetails =await _srvice.GetByIdAsync(id);
            if (ActorDetails == null) return View("NotFound");
            return View(ActorDetails);
        }
        //Get:Actor/Update
        public async Task<IActionResult> Edit(int id)
        {
            var ActorDetails = await _srvice.GetByIdAsync(id);
            if (ActorDetails == null) return View("NotFound");
            return View(ActorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Actor actor)
        {
            if (!ModelState.IsValid)
            {
                // Model state is not valid, return a BadRequest with error messages
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var errorMessage = string.Join(" ", errorMessages);
                return BadRequest(errorMessage);
            }
            await _srvice.UpdateAsync(id, actor);

            return RedirectToAction(nameof(Index));
        }
        //Get:Actor/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ActorDetails = await _srvice.GetByIdAsync(id);
            if (ActorDetails == null) return View("NotFound");
            return View(ActorDetails);
        }

        [HttpPost ,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ActorDetails = await _srvice.GetByIdAsync(id);
            if (ActorDetails == null) return View("NotFound");

            await _srvice.DeleteAsync(id);
        
            return RedirectToAction(nameof(Index));
        }

    }
}
