using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class AppDbCContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbCContext(DbContextOptions<AppDbCContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LaptopBrand>().HasKey(lb => new
            {
                lb.LaptopId,
                lb.BrandId
            });

            modelBuilder.Entity<LaptopBrand>().HasOne(l => l.Laptop).WithMany(lb => lb.LaptopBrands).HasForeignKey(l => l.LaptopId);

            modelBuilder.Entity<LaptopBrand>().HasOne(l => l.Brand).WithMany(lb => lb.LaptopBrands).HasForeignKey(l => l.BrandId);

            modelBuilder.Entity<Laptop>()
                        .HasOne(l => l.LaptopsTypes)
                        .WithMany(lt => lt.Laptops)
                        .HasForeignKey(l => l.LaptopsTypesId);



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Laptop> Laptops { get; set; }

        public DbSet<LaptopBrand> LaptopBrands { get; set; }

        public DbSet<LaptopTypes> LaptopTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemNew> OrderItemsNew { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<ShoppingCartItemNew> ShoppingCartItemsNew { get; set; }

        public DbSet<Components> Components { get; set; }
    }
}
