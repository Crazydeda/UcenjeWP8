namespace WebShopAPI.Models
{
    public class Kosarica
    {
        public int ID_kosarice { get; set; }
        public int ID_korisnika { get; set; } = 1;
        public DateTime datum_kreiranja { get; set; } = DateTime.Now;
        public string status { get; set; }
    }
}