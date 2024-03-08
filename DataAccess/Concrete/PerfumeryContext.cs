using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class PerfumeryContext : DbContext
    {
        public PerfumeryContext(DbContextOptions options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Perfume> Perfumes { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityHelper.SeedBrands(modelBuilder);
            EntityHelper.SeedPerfumes(modelBuilder);
            EntityHelper.SeedUserDetils(modelBuilder);
        }
    }
}
