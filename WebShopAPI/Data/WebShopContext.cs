using Microsoft.EntityFrameworkCore;
using WebShopAPI.Models;

namespace WebShopAPI.Data
{
    public class WebShopContext : DbContext
    {
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options) {}

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Kosarica> Kosarice { get; set; }
        public DbSet<StavkaKosarice> StavkeKosarice { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }
    }
}