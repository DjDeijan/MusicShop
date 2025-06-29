namespace MusicShop.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int InstrumentId { get; set; }
        public int Rating { get; set; }
        public Instrument Instrument { get; set; }
    }
}
