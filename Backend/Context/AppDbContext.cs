using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> User { get; set; }
    }
}
