using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("stavke_kosarice")]
    public class StavkaKosarice
    {
        [Key]
        [Column("ID_stavke")]
        public int ID_stavke { get; set; }

        [Column("ID_kosarice")]
        public int ID_kosarice { get; set; }

        [Column("ID_proizvoda")]
        public int ID_proizvoda { get; set; }

        [Column("kolicina")]
        public int Kolicina { get; set; }

        [Column("cijena_kom", TypeName = "decimal(10,2)")]
        public decimal CijenaKom { get; set; }

        [ForeignKey(nameof(ID_kosarice))]
        public Kosarica? Kosarica { get; set; }

        [ForeignKey(nameof(ID_proizvoda))]
        public Proizvod? Proizvod { get; set; }
    }
}
