using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("stavke_narudzbe")]
    public class StavkaNarudzbe
    {
        [Key]
        [Column("ID_stavke_narudzbe")]
        public int ID_stavke_narudzbe { get; set; }

        [Column("ID_narudzbe")]
        public int ID_narudzbe { get; set; }

        [Column("ID_proizvoda")]
        public int ID_proizvoda { get; set; }

        [Column("kolicina")]
        public int Kolicina { get; set; }

        [Column("cijena_kom", TypeName = "decimal(10,2)")]
        public decimal CijenaKom { get; set; }

        [ForeignKey(nameof(ID_narudzbe))]
        public Narudzba? Narudzba { get; set; }

        [ForeignKey(nameof(ID_proizvoda))]
        public Proizvod? Proizvod { get; set; }
    }
}
