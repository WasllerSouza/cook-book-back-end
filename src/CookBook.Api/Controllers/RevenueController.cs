using CookBook.Api.Filters;
using CookBook.Application.UseCases.DashBoard;
using CookBook.Application.UseCases.Revenue.Register;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers;

[ApiController]
[Route("revenue")]
[ServiceFilter(typeof(AuthenticatedUser))]
public class RevenueController : ControllerBase
{
    [HttpPost("register", Name = "revenue-register")]
    [ProducesResponseType(typeof(GenericResponse<RevenueResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterRevenue(
             [FromServices] IRevenueRegisterUseCase useCase,
             [FromBody] RevenueRequest request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }

    [HttpPost("search", Name = "revenue-search")]
    [ProducesResponseType(typeof(GenericResponse<IList<DashBoardResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SearchRevenue(
             [FromServices] IDashBoardUseCase useCase,
             [FromBody] DashBoardRequest request
        )
    {
        GenericResponse<IList<DashBoardResponse>> response = await useCase.Execute(request);

        if (response.Data.Any())
        {
            return Ok(response);
        }

        return NoContent();

    }
}
