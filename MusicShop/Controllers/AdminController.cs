using Microsoft.AspNetCore.Mvc;
using MusicShop.Services;
using MusicShop.Entities;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Data;

namespace MusicShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly IMusicShopService _service;

        public AdminController(IMusicShopService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var instruments = _service.GetAllInstrumentModels();
            return View(instruments);
        }

        public IActionResult Orders()
        {
            var orders = _service.GetOrders();
            return View(orders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _service.AddInstrument(instrument);
                return RedirectToAction("Index");
            }
            return View(instrument);
        }

        public IActionResult Edit(int id)
        {
            var instrument = _service.GetInstrumentModel(id);
            if (instrument == null) return NotFound();
            return View(instrument);
        }

        [HttpPost]
        public IActionResult Edit(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateInstrument(instrument);
                return RedirectToAction("Index");
            }
            return View(instrument);
        }

        public IActionResult Delete(int id)
        {
            _service.DeleteInstrument(id);
            return RedirectToAction("Index");
        }
    }
}
