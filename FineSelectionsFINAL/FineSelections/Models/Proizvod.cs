using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("proizvodi")]
    public class Proizvod
    {
        [Key]
        [Column("ID_proizvoda")]
        public int ID_proizvoda { get; set; }

        [Column("naziv")]
        public string? Naziv { get; set; }

        [Column("vrsta")]
        public string? Vrsta { get; set; }

        [Column("opis")]
        public string? Opis { get; set; }

        [Column("cijena", TypeName = "decimal(10,2)")]
        public decimal Cijena { get; set; }

        [Column("zaliha")]
        public int Zaliha { get; set; }

        [Column("slika")]
        public string? Slika { get; set; }
    }
}
