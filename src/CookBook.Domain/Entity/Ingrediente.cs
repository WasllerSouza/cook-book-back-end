namespace CookBook.Domain.Entity;
public class Ingrediente : EntityBase
{
    public string Produto { get; set; }
    public string Quantidade { get; set; }
    public long ReceitaId { get; set; }
}
