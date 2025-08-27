using FineSelections.Models;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Data
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options) : base(options) { }

        public DbSet<Korisnik> Korisnici => Set<Korisnik>();
        public DbSet<Proizvod> Proizvodi => Set<Proizvod>();
        public DbSet<Kosarica> Kosarice => Set<Kosarica>();
        public DbSet<StavkaKosarice> StavkeKosarice => Set<StavkaKosarice>();
        public DbSet<Narudzba> Narudzbe => Set<Narudzba>();
        public DbSet<StavkaNarudzbe> StavkeNarudzbe => Set<StavkaNarudzbe>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Keep default table & column names via DataAnnotations.
        }
    }
}
