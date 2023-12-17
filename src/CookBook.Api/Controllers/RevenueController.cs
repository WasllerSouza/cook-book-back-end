using CookBook.Api.Filters;
using CookBook.Application.UseCases.Revenue.Register;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers;

[ApiController]
[Route("revenue")]
[ServiceFilter(typeof(AuthenticatedUser))]
public class RevenueController : ControllerBase
{
    [HttpPost("register", Name = "revenue-register")]
    [ProducesResponseType(typeof(RevenueResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterRevenue(
             [FromServices] IRevenueRegisterUseCase useCase,
             [FromBody] RevenueRequest request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }
}
