using Microsoft.EntityFrameworkCore;
using TaskTest.API.Entities;

namespace TaskTest.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}