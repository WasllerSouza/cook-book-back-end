using CookBook.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entity;

[Table("Receitas")]
public class Receita : EntityBase
{ 
    public String Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public String ModoPreparo { get; set; }
    public ICollection<Ingrediente> Ingredientes { get; set; }
    public Guid UsuarioId { get; set; }
}
