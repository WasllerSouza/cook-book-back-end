using CookBook.Api.Filters;
using CookBook.Application.UseCases.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers;

[ApiController]
[ServiceFilter(typeof(AuthenticatedUser))]
public class TokenController : ControllerBase
{
    [HttpGet("validate-token", Name = "validate-token")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterRevenue(
             [FromServices] ITokenUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);

    }
}
