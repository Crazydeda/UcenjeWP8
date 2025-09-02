using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("kosarice")]
    public class Kosarica
    {
        [Key]
        [Column("ID_kosarice")]
        public int IdKosarice { get; set; }

        [Column("ID_korisnika")]
        public int? IdKorisnika { get; set; }

        [Column("datum_kreiranja")]
        public DateTime DatumKreiranja { get; set; } = DateTime.Now;

        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; } // aktivna, naručena

        // Navigation properties
        [ForeignKey("IdKorisnika")]
        public virtual Korisnik? Korisnik { get; set; }
        
        public virtual ICollection<StavkaKosarice> StavkeKosarice { get; set; } = new List<StavkaKosarice>();
    }
}
