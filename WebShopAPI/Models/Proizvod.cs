namespace WebShopAPI.Models
{
    public class Proizvod
    {
        public int ID_proizvoda { get; set; }
        public string naziv { get; set; }
        public string vrsta { get; set; }
        public string opis { get; set; }
        public decimal cijena { get; set; }
        public int zaliha { get; set; }
    }
}