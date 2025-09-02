using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("narudzbe")]
    public class Narudzba
    {
        [Key]
        [Column("ID_narudzbe")]
        public int IdNarudzbe { get; set; }

        [Column("ID_korisnika")]
        public int? IdKorisnika { get; set; }

        [Column("datum_narudzbe")]
        public DateTime DatumNarudzbe { get; set; } = DateTime.Now;

        [Column("ukupna_cijena", TypeName = "decimal(10,2)")]
        public decimal? UkupnaCijena { get; set; }

        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; } // u obradi, isporučena, otkazana

        // Navigation properties
        [ForeignKey("IdKorisnika")]
        public virtual Korisnik? Korisnik { get; set; }
        
        public virtual ICollection<StavkaNarudzbe> StavkeNarudzbe { get; set; } = new List<StavkaNarudzbe>();
    }
}
