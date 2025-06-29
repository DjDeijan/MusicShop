using Microsoft.EntityFrameworkCore;
using MusicShop.Entities;

namespace MusicShop.Models
{
    public class MusicShopContext : DbContext
    {
        public MusicShopContext(DbContextOptions<MusicShopContext> options) : base(options) { }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
