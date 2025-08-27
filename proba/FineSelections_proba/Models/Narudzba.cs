using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineSelections.Models
{
    [Table("narudzbe")]
    public class Narudzba
    {
        [Key]
        [Column("ID_narudzbe")]
        public int ID_narudzbe { get; set; }

        [Column("ID_korisnika")]
        public int ID_korisnika { get; set; }

        [Column("datum_narudzbe")]
        public DateTime DatumNarudzbe { get; set; } = DateTime.Now;

        [Column("ukupna_cijena", TypeName = "decimal(10,2)")]
        public decimal UkupnaCijena { get; set; }

        [Column("status")]
        public string Status { get; set; } = "u obradi";

        [ForeignKey(nameof(ID_korisnika))]
        public Korisnik? Korisnik { get; set; }

        public ICollection<StavkaNarudzbe>? Stavke { get; set; }
    }
}
