using colectare_deseuri.Models;
using Microsoft.EntityFrameworkCore;

namespace colectare_deseuri.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Date> Colectari { get; set; } 
    }
}
