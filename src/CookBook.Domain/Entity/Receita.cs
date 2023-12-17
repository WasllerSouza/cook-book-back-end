using CookBook.Domain.Enum;

namespace CookBook.Domain.Entity;
public class Receita : EntityBase
{ 
    public String Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public String ModoPreparo { get; set; }
    public ICollection<Ingrediente> Ingredientes { get; set; }
}
