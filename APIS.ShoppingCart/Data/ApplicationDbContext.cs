using APIS.ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace APIS.ShoppingCart.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }

        public DbSet<CartDetails> cartDetails { get; set; }
        public DbSet<CartHeader> cartHeaders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
        }
    }
    }
