
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("proizvodi")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        [Column("naziv")]
        public string Naziv { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        [Column("vrsta")]
        public string Vrsta { get; set; } = string.Empty; // viski, rum, gin, konjak...

        [Column("opis")]
        public string? Opis { get; set; }

        [Column("cijena")]
        [DataType(DataType.Currency)]
        public decimal Cijena { get; set; }

        [Column("zaliha")]
        public int Zaliha { get; set; }

        [Column("slika")]
        public string? Slika { get; set; }
    }
}
