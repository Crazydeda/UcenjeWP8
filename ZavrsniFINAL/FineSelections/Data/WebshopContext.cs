using Microsoft.EntityFrameworkCore;
using FineSelections.Models;

namespace FineSelections.Data
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options) : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici { get; set; } = null!;
        public DbSet<Proizvod> Proizvodi { get; set; } = null!;
        public DbSet<Kosarica> Kosarice { get; set; } = null!;
        public DbSet<StavkaKosarice> StavkeKosarice { get; set; } = null!;
        public DbSet<Narudzba> Narudzbe { get; set; } = null!;
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Kosarica>()
                .HasOne(k => k.Korisnik)
                .WithMany()
                .HasForeignKey(k => k.IdKorisnika)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StavkaKosarice>()
                .HasOne(sk => sk.Proizvod)
                .WithMany()
                .HasForeignKey(sk => sk.IdProizvoda)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure the relationship between Kosarica and StavkaKosarice
            modelBuilder.Entity<Kosarica>()
                .HasMany(k => k.StavkeKosarice)
                .WithOne()
                .HasForeignKey(sk => sk.IdKosarice)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Korisnik)
                .WithMany()
                .HasForeignKey(n => n.IdKorisnika)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StavkaNarudzbe>()
                .HasOne(sn => sn.Proizvod)
                .WithMany()
                .HasForeignKey(sn => sn.IdProizvoda)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StavkaNarudzbe>()
                .HasOne(sn => sn.Narudzba)
                .WithMany(n => n.StavkeNarudzbe)
                .HasForeignKey(sn => sn.IdNarudzbe)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure unique constraints
            modelBuilder.Entity<Korisnik>()
                .HasIndex(k => k.Email)
                .IsUnique();

            // Configure default values
            modelBuilder.Entity<Kosarica>()
                .Property(k => k.DatumKreiranja)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Narudzba>()
                .Property(n => n.DatumNarudzbe)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
