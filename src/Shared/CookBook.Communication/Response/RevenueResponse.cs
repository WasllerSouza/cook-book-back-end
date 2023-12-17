using CookBook.Communication.Enum;

namespace CookBook.Communication.Response;
public class RevenueResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoPreparo { get; set; }
    public List<IngredientResponse> Ingredientes { get; set; }

    public RevenueResponse()
    {
        Ingredientes = new();
    }
}
