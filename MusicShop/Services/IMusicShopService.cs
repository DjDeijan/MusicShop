using System.Collections.Generic;
using MusicShop.Entities;
using MusicShop.Models;

namespace MusicShop.Services
{
    public interface IMusicShopService
    {
        List<InstrumentModel> GetAllInstrumentModels();
        InstrumentModel GetInstrumentModel(int id);
        void AddOrder(Order order);
        int AddReview(Review review);
        void AddInstrument(Instrument instrument);
        void UpdateInstrument(Instrument instrument);
        void DeleteInstrument(int id);
        List<Order> GetOrders();
        public IList<Review> GetReviews(int instrumentId, int lastReviewId, int skip, int take);
    }
}
