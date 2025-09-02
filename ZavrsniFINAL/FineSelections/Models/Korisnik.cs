using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("korisnici")]
    public class Korisnik
    {
        [Key]
        [Column("ID_korisnika")]
        public int IdKorisnika { get; set; }

        [Column("ime")]
        [StringLength(50)]
        public string? Ime { get; set; }

        [Column("prezime")]
        [StringLength(50)]
        public string? Prezime { get; set; }

        [Column("god_rod")]
        public DateTime? GodRod { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("adresa")]
        [StringLength(255)]
        public string? Adresa { get; set; }

        [Column("telefon")]
        [StringLength(20)]
        public string? Telefon { get; set; }
    }
}
