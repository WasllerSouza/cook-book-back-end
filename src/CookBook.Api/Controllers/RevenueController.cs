using CookBook.Api.Filters;
using CookBook.Application.UseCases.DashBoard;
using CookBook.Application.UseCases.Revenue.Register;
using CookBook.Application.UseCases.Revenue.Search;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    [HttpGet("find", Name = "revenue-find-by-id")]
    [ProducesResponseType(typeof(GenericResponse<RevenueResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchRevenueById(
             [FromServices] IRevenueSearchUseCase useCase,
             [FromQuery] string Id
        )
    {
        GenericResponse<RevenueResponse> response = await useCase.Execute(new Guid(Id));

        return Ok(response);

    }
}
