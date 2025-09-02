using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("stavke_narudzbe")]
    public class StavkaNarudzbe
    {
        [Key]
        [Column("ID_stavke_narudzbe")]
        public int IdStavkeNarudzbe { get; set; }

        [Column("ID_narudzbe")]
        public int? IdNarudzbe { get; set; }

        [Column("ID_proizvoda")]
        public int? IdProizvoda { get; set; }

        [Column("kolicina")]
        public int? Kolicina { get; set; }

        [Column("cijena_kom", TypeName = "decimal(10,2)")]
        public decimal? CijenaKom { get; set; }

        // Navigation properties
        [ForeignKey("IdNarudzbe")]
        public virtual Narudzba? Narudzba { get; set; }

        [ForeignKey("IdProizvoda")]
        public virtual Proizvod? Proizvod { get; set; }
    }
}
