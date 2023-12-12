using CookBook.Application.UseCases.User.Register;
using CookBook.Application.UseCases.User.SingIn;
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
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] IUserRegisterUseCase useCase,
            [FromBody] UserRegisterRequest request)
        {
            await useCase.Execute(request, HttpContext.Response.Cookies);
            
            return Created(string.Empty, null);

        }
        
        [HttpPost("login", Name = "login")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> SingInUser(
            [FromServices] ISingInUseCase useCase,
            [FromBody] UserSingInRequest request)
        {
            await useCase.Execute(request, HttpContext.Response.Cookies);
            
            return Ok();

        }

    }
}