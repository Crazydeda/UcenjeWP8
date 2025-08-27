using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("kosarice")]
    public class Kosarica
    {
        [Key]
        [Column("ID_kosarice")]
        public int ID_kosarice { get; set; }

        [Column("ID_korisnika")]
        public int ID_korisnika { get; set; }

        [Column("datum_kreiranja")]
        public DateTime DatumKreiranja { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "aktivna";

        [ForeignKey(nameof(ID_korisnika))]
        public Korisnik? Korisnik { get; set; }

        public ICollection<StavkaKosarice>? Stavke { get; set; }
    }
}
