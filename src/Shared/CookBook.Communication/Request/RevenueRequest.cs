using CookBook.Communication.Enum;

namespace CookBook.Communication.Request;
public class RevenueRequest
{
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoPreparo { get; set; }
    public List<IngredientRequest> Ingredientes { get; set; }

    public RevenueRequest()
    {
        Ingredientes = new ();
    }
}
