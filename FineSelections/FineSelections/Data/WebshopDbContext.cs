using FineSelections.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Web.Data;

public class WebshopDbContext : DbContext
{
    public WebshopDbContext(DbContextOptions<WebshopDbContext> options) : base(options) { }

    public DbSet<Proizvod> Proizvodi => Set<Proizvod>();
    public DbSet<KorisnikProfil> Korisnici => Set<KorisnikProfil>();
    public DbSet<Kosarica> Kosarice => Set<Kosarica>();
    public DbSet<StavkaKosarice> StavkeKosarice => Set<StavkaKosarice>();
    public DbSet<Narudzba> Narudzbe => Set<Narudzba>();
    public DbSet<StavkaNarudzbe> StavkeNarudzbe => Set<StavkaNarudzbe>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Proizvod>(e =>
        {
            e.ToTable("proizvodi");
            e.HasKey(x => x.ID_proizvoda);
            e.Property(x => x.ID_proizvoda).ValueGeneratedOnAdd();
            e.Property(x => x.naziv).HasMaxLength(100);
            e.Property(x => x.vrsta).HasMaxLength(50);
            e.Property(x => x.opis).HasMaxLength(500);
            e.Property(x => x.cijena).HasColumnType("decimal(10,2)");
            e.Property(x => x.slika).HasMaxLength(355);
        });

        modelBuilder.Entity<KorisnikProfil>(e =>
        {
            e.ToTable("korisnici");
            e.HasKey(x => x.ID_korisnika);
            e.Property(x => x.ID_korisnika).ValueGeneratedOnAdd();
            e.Property(x => x.ime).HasMaxLength(50);
            e.Property(x => x.prezime).HasMaxLength(50);
            e.Property(x => x.email).HasMaxLength(100);
            e.Property(x => x.adresa).HasMaxLength(255);
            e.Property(x => x.telefon).HasMaxLength(20);
        });

        modelBuilder.Entity<Kosarica>(e =>
        {
            e.ToTable("kosarice");
            e.HasKey(x => x.ID_kosarice);
            e.Property(x => x.ID_kosarice).ValueGeneratedOnAdd();
            e.Property(x => x.status).HasMaxLength(20);
            e.HasOne<KorisnikProfil>()
                .WithMany()
                .HasForeignKey(x => x.ID_korisnika)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StavkaKosarice>(e =>
        {
            e.ToTable("stavke_kosarice");
            e.HasKey(x => x.ID_stavke);
            e.Property(x => x.ID_stavke).ValueGeneratedOnAdd();
            e.HasOne<Kosarica>()
                .WithMany(x => x.Stavke)
                .HasForeignKey(x => x.ID_kosarice)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Proizvod>()
                .WithMany()
                .HasForeignKey(x => x.ID_proizvoda)
                .OnDelete(DeleteBehavior.Cascade);
            e.Property(x => x.cijena_kom).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Narudzba>(e =>
        {
            e.ToTable("narudzbe");
            e.HasKey(x => x.ID_narudzbe);
            e.Property(x => x.ID_narudzbe).ValueGeneratedOnAdd();
            e.Property(x => x.status).HasMaxLength(20);
            e.Property(x => x.ukupna_cijena).HasColumnType("decimal(10,2)");
            e.HasOne<KorisnikProfil>()
                .WithMany()
                .HasForeignKey(x => x.ID_korisnika)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StavkaNarudzbe>(e =>
        {
            e.ToTable("stavke_narudzbe");
            e.HasKey(x => x.ID_stavke_narudzbe);
            e.Property(x => x.ID_stavke_narudzbe).ValueGeneratedOnAdd();
            e.HasOne<Narudzba>()
                .WithMany(x => x.Stavke)
                .HasForeignKey(x => x.ID_narudzbe)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Proizvod>()
                .WithMany()
                .HasForeignKey(x => x.ID_proizvoda)
                .OnDelete(DeleteBehavior.Cascade);
            e.Property(x => x.cijena_kom).HasColumnType("decimal(10,2)");
        });
    }
}
