using CookBook.Application.UseCases.User.Register;
using CookBook.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {

        [HttpPost("register", Name = "register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] IUserRegisterUseCase useCase,
            [FromBody] UserRegisterRequest request)
        {
            var genericResponse = await useCase.Execute(request, HttpContext.Response.Cookies);
            
            return Created(string.Empty, genericResponse);

        }

    }
}