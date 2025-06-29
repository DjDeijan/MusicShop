using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Entities;
using MusicShop.Models;

namespace MusicShop.Services
{
    public class MusicShopService : IMusicShopService
    {
        private readonly MusicShopContext _context;

        public MusicShopService(MusicShopContext context)
        {
            _context = context;
        }

        public List<InstrumentModel> GetAllInstrumentModels()
        {
            return _context.Instruments
                .Select(x => new InstrumentModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    PurchaseCount = x.Orders.Count,
                    ReviewCount = x.Reviews.Count,
                    AverageRating = Math.Round((decimal)x.Reviews.Select(y => y.Rating).Sum() / Math.Max(x.Reviews.Count, 1), 1),
                    OneStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 1).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                    TwoStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 2).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                    ThreeStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 3).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                    FourStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 4).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                    FiveStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 5).Count() / Math.Max(x.Reviews.Count, 1) * 100
                })
                .ToList();
        }

        public InstrumentModel GetInstrumentModel(int id)
        {
            return _context.Instruments.Include(x => x.Reviews).Include(x => x.Orders).Select(x => new InstrumentModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                PurchaseCount = x.Orders.Count,
                ReviewCount = x.Reviews.Count,
                AverageRating = Math.Round((decimal)x.Reviews.Select(y => y.Rating).Sum() / Math.Max(x.Reviews.Count, 1), 1),
                OneStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 1).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                TwoStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 2).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                ThreeStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 3).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                FourStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 4).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                FiveStarRatings = (decimal)x.Reviews.Where(y => y.Rating == 5).Count() / Math.Max(x.Reviews.Count, 1) * 100,
                Reviews = x.Reviews.OrderByDescending(y => y.Id).Take(5).ToList(),
                LastReviewId = x.Reviews.Select(y => y.Id).OrderBy(y => y).LastOrDefault()
            }).FirstOrDefault(i => i.Id == id);
        }

        public void AddOrder(Order order)
        {
            var instrument = _context.Instruments.Find(order.InstrumentId);
            if (instrument != null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }

        public int AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review.Id;
        }

        public void AddInstrument(Instrument instrument)
        {
            _context.Instruments.Add(instrument);
            _context.SaveChanges();
        }

        public void UpdateInstrument(Instrument instrument)
        {
            _context.Instruments.Update(instrument);
            _context.SaveChanges();
        }

        public void DeleteInstrument(int id)
        {
            var instrument = _context.Instruments.Find(id);
            if (instrument != null)
            {
                _context.Instruments.Remove(instrument);
                _context.SaveChanges();
            }
        }

        public List<Order> GetOrders() => _context.Orders.Include(o => o.Instrument).ToList();

        public IList<Review> GetReviews(int instrumentId, int lastReviewId, int skip, int take)
        {
            return _context.Reviews
                .Where(x => x.InstrumentId == instrumentId && x.Id <= lastReviewId)
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

    }
}
