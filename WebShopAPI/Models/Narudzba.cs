namespace WebShopAPI.Models
{
    public class Narudzba
    {
        public int ID_narudzbe { get; set; }
        public int ID_korisnika { get; set; } = 1;
        public DateTime datum_narudzbe { get; set; } = DateTime.Now;
        public decimal ukupna_cijena { get; set; }
        public string status { get; set; }
    }
}