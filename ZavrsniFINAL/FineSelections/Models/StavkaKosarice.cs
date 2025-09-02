using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("stavke_kosarice")]
    public class StavkaKosarice
    {
        [Key]
        [Column("ID_stavke")]
        public int IdStavke { get; set; }

        [Column("ID_kosarice")]
        public int? IdKosarice { get; set; }

        [Column("ID_proizvoda")]
        public int? IdProizvoda { get; set; }

        [Column("kolicina")]
        public int? Kolicina { get; set; }

        [Column("cijena_kom", TypeName = "decimal(10,2)")]
        public decimal? CijenaKom { get; set; }

        [ForeignKey("IdProizvoda")]
        public virtual Proizvod? Proizvod { get; set; }
    }
}
