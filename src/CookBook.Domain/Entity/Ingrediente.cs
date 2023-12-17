using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entity;

[Table("Ingredientes")]
public class Ingrediente : EntityBase
{
    public string Produto { get; set; }
    public string Quantidade { get; set; }
    public Guid ReceitaId { get; set; }
}
