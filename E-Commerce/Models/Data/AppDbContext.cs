using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace E_Commerce.Models.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Wishlistitem>(x => x.HasKey(k => (new { k.ProductId, k.whishlistId })));
            builder.Entity<WishList>().ToTable("WishLists");
            builder.Entity<Wishlistitem>().ToTable("WishlistItems");
            builder.Entity<Transaction>()
     .HasOne(t => t.order)
     .WithMany(o => o.transactions)
     .HasForeignKey(t => t.OrderID)
     .OnDelete(DeleteBehavior.Cascade); // Enable cascade delete here

            // Handle other relationships that may create multiple cascade paths
            builder.Entity<Order>()
                .HasOne(o => o.client)
                .WithMany(c => c.orders) // Example for client relationship
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict or SetNull to avoid cascade conflict // Enables cascade delete

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Client>Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction>Transactions { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<WishList> WhishLists { get; set; }
        public DbSet<Wishlistitem> WhishListItems { get; set;}

    }
}
