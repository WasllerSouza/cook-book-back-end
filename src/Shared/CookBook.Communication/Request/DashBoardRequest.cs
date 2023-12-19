using CookBook.Communication.Enum;

namespace CookBook.Communication.Request;
public class DashBoardRequest
{
    public string Search { get; set; }
    public Categoria? Filter { get; set; }
}
