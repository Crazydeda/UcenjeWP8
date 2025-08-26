namespace FineSelections.Web.Models;

public class Narudzba
{
    public int ID_narudzbe { get; set; }
    public int ID_korisnika { get; set; }
    public DateTime datum_narudzbe { get; set; }
    public decimal ukupna_cijena { get; set; }
    public string? status { get; set; }
    public List<StavkaNarudzbe> Stavke { get; set; } = new();
}
