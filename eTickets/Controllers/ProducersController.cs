using eTickets.date;
using eTickets.date.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public async Task<IActionResult> Index()
        {
            var AllProducers = await _producerService.GetAllAsync();

            return View(AllProducers);
        }
        //Get : Poducer/details
        public async Task<IActionResult> Details(int id)
        {
            var ProducerDetails = await _producerService.GetByIdAsync(id);
            if (ProducerDetails != null)
            {
                return View(ProducerDetails);
            }
            else
                return View("NotFound");
        }
        //Get:poducer/create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);

            }
            await _producerService.AddAsync(producer);
            return RedirectToAction(nameof(Index));


        }
        //Get:poducer/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var ProducerDeatils = await _producerService.GetByIdAsync(id);
            if (ProducerDeatils == null)
            {
                return View("NotFound");
            }
            return View(ProducerDeatils);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
         
            if (id == producer.Id)
            {
                await _producerService.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);


        }
        //Get:poducer/delete
        public async Task<IActionResult> Delete(int id)
        {
            var ProducerDeatils = await _producerService.GetByIdAsync(id);
            if (ProducerDeatils == null)
            {
                return View("NotFound");
            }
            return View(ProducerDeatils);
        }
        [HttpPost,ActionName("Delete")]
       

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var ProducerDeatils = await _producerService.GetByIdAsync(id);
            if (ProducerDeatils == null)
            {
                return View("NotFound");
            }
            await _producerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));


        }

    }
}
