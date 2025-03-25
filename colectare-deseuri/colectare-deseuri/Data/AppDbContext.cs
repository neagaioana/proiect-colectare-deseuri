using colectare_deseuri.Models;
using Microsoft.EntityFrameworkCore;

namespace colectare_deseuri.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cetatean> Cetateni { get; set; }
        public DbSet<Pubela> Pubele { get; set; }
        public DbSet<Colectare> Colectari { get; set; }
        public DbSet<PubelaCetatean> PubeleCetateni { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PubelaCetatean>()
                .HasKey(pc => new { pc.IdPubela, pc.IdCetatean });

            modelBuilder.Entity<PubelaCetatean>()
                .HasOne(pc => pc.Cetatean)
                .WithMany(c => c.PubeleCetateni)
                .HasForeignKey(pc => pc.IdCetatean);

            modelBuilder.Entity<PubelaCetatean>()
                .HasOne(pc => pc.Pubela)
                .WithMany(p => p.PubeleCetateni)
                .HasForeignKey(pc => pc.IdPubela);
        }
    }
}
