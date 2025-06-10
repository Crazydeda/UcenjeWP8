namespace WebShopAPI.Models
{
    public class Korisnik
    {
        public int ID_korisnika { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public DateTime god_rod { get; set; }
        public string email { get; set; }
        public string adresa { get; set; }
        public string telefon { get; set; }
    }
}