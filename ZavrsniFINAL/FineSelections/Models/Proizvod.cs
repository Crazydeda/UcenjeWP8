using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("proizvodi")]
    public class Proizvod
    {
        [Key]
        [Column("ID_proizvoda")]
        public int IdProizvoda { get; set; }

        [Column("naziv")]
        [StringLength(100)]
        public string? Naziv { get; set; }

        [Column("vrsta")]
        [StringLength(50)]
        public string? Vrsta { get; set; } // viski, rum, gin, konjak

        [Column("opis")]
        [StringLength(500)]
        public string? Opis { get; set; }

        [Column("cijena", TypeName = "decimal(10,2)")]
        public decimal? Cijena { get; set; }

        [Column("zaliha")]
        public int? Zaliha { get; set; }

        [Column("slika")]
        [StringLength(355)]
        public string? Slika { get; set; }
    }
}
