using System;

namespace MusicShop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
