using CookBook.Application.UseCases.User.Register;
using CookBook.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var token = await useCase.Execute(request);

            return Created(string.Empty, token);

        }
        
    }
}