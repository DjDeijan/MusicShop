using MusicShop.Entities;

namespace MusicShop.Models
{
    public class InstrumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int PurchaseCount { get; set; }
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }
        public decimal OneStarRatings {get; set; }
        public decimal TwoStarRatings { get; set; }
        public decimal ThreeStarRatings { get; set; }
        public decimal FourStarRatings { get; set; }
        public decimal FiveStarRatings { get; set; }
        public int LastReviewId { get; set; }
        public IList<Review> Reviews { get; set; }
    }
}
