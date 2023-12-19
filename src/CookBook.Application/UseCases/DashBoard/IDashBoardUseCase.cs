using CookBook.Communication.Request;
using CookBook.Communication.Response;

namespace CookBook.Application.UseCases.DashBoard;
public interface IDashBoardUseCase
{
    Task<GenericResponse<IList<DashBoardResponse>>> Execute(DashBoardRequest request);
}
