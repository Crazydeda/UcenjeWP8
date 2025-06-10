namespace WebShopAPI.Models
{
    public class StavkaNarudzbe
    {
        public int ID_stavke_narudzbe { get; set; }
        public int ID_narudzbe { get; set; }
        public int ID_proizvoda { get; set; }
        public int kolicina { get; set; }
        public decimal cijena_kom { get; set; }
    }
}