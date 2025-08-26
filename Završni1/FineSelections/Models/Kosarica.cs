namespace FineSelections.Web.Models;

public class Kosarica
{
    public int ID_kosarice { get; set; }
    public int ID_korisnika { get; set; }
    public DateTime datum_kreiranja { get; set; }
    public string? status { get; set; }
    public List<StavkaKosarice> Stavke { get; set; } = new();
}
