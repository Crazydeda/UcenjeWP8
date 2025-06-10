namespace WebShopAPI.Models
{
    public class StavkaKosarice
    {
        public int ID_stavke { get; set; }
        public int ID_kosarice { get; set; }
        public int ID_proizvoda { get; set; }
        public int kolicina { get; set; }
        public decimal cijena_kom { get; set; }
    }
}