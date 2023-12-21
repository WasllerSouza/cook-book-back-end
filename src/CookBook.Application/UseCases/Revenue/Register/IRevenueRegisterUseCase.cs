using CookBook.Communication.Request;
using CookBook.Communication.Response;

namespace CookBook.Application.UseCases.Revenue.Register;
public interface IRevenueRegisterUseCase
{
    Task<GenericResponse<RevenueResponse>> Execute(RevenueRequest request);
}
