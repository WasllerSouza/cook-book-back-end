using CookBook.Api.Filters;
using CookBook.Application.UseCases.User.RecoveryPassword;
using CookBook.Application.UseCases.User.Register;
using CookBook.Application.UseCases.User.SingIn;
using CookBook.Communication.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

        [HttpPost("register", Name = "user-register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] IUserRegisterUseCase useCase,
            [FromBody] UserRegisterRequest request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);

        }

        [HttpPost("login", Name = "user-login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> SingInUser(
            [FromServices] ISingInUseCase useCase,
            [FromBody] UserSingInRequest request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);

        }
        
        [HttpPut("recovery-password", Name = "user-recovery-password")]
        [ServiceFilter(typeof(AuthenticatedUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RecoveryPasswordUser(
            [FromServices] IRecoveryPasswordUseCase useCase,
            [FromBody] UserRecoveryPasswordRequest request)
        {
            await useCase.Execute(request);

            return Ok();

        }

    }
}