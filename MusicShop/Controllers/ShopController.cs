using Microsoft.AspNetCore.Mvc;
using MusicShop.Entities;
using MusicShop.Models;
using MusicShop.Services;
using System.Diagnostics;

namespace MusicShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IMusicShopService _service;

        public ShopController(IMusicShopService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var instruments = _service.GetAllInstrumentModels();
            return View(instruments);
        }

        public IActionResult Details(int id)
        {
            var instrument = _service.GetInstrumentModel(id);
            if (instrument == null) return NotFound();
            return View(instrument);
        }

        [HttpPost]
        public IActionResult Order(int instrumentId, string customerName, string email)
        {
            var order = new Order
            {
                CustomerName = customerName,
                Email = email,
                InstrumentId = instrumentId,
                OrderDate = DateTime.UtcNow
            };
            _service.AddOrder(order);

            return Json(new { message = $"{customerName}, thank you for your order!" });
        }

        [HttpPost]
        public IActionResult AddReview(int instrumentId, string customerName, string content, int rating)
        {
            var review = new Review
            {
                InstrumentId = instrumentId,
                UserName = customerName,
                Content = content,
                Rating = rating
            };
            var id = _service.AddReview(review);

            return Json(new { id });
        }

        public IActionResult GetReviews(int instrumentId, int lastReviewId, int skip, int take)
        {
            var reviews = _service.GetReviews(instrumentId, lastReviewId, skip, take);

            return PartialView("_Reviews", reviews);
        }

        public IActionResult GetReviewSummary(int instrumentId)
        {
            var instrumentModel = _service.GetInstrumentModel(instrumentId);

            return PartialView("_ReviewSummary", instrumentModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
