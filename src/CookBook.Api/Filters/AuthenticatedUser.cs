using CookBook.Application.Services.Token;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Exceptions;
using FactoryMethod.ConcreteCreator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Strategy.ConcreteStrategy;
using Strategy.Context;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CookBook.Api.Filters;

public class AuthenticatedUser : AuthorizeAttribute, IAsyncAuthorizationFilter
{

    private readonly TokenService _tokenController;
    private readonly IUsuarioReadOnlyRepository _repository;

    public AuthenticatedUser(TokenService tokenController, IUsuarioReadOnlyRepository repository)
    {
        _tokenController = tokenController;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = ExistTokenInRequest(context);
            var email = _tokenController.GetEmailBySession(token);

            var usuario = await _repository.GetUserByEmail(email);

            if (usuario is null)
            {
                var contextStrategy = new Context(new ConcreteStrategyAuthException());
                contextStrategy.ThrowException(new List<string>
                {
                    ResourceMessageError.TOKEN_EXPIRADO
                });
            }
        }
        catch (SecurityTokenExpiredException)
        {
            ExpiredToken(context);
        }
        catch
        {
            UserAccessForbidden(context);
        }

    }

    private string ExistTokenInRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorization))
        {
            var contextStrategy = new Context(new ConcreteStrategyAuthException());
            contextStrategy.ThrowException(new List<string>
                {
                    ResourceMessageError.TOKEN_EXPIRADO
                });
        }

        return authorization["Bearer".Length..].Trim();
    }

    private void ExpiredToken(AuthorizationFilterContext context)
    {

        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Errors = new List<string>
        {
            ResourceMessageError.TOKEN_EXPIRADO
        };
        dynamicResponse.StatusCode = (int)HttpStatusCode.Unauthorized;

        context.Result = new UnauthorizedObjectResult(new ConcreteCreatorErrorResponse().SomeOperation(dynamicResponse));
    }

    private void UserAccessForbidden(AuthorizationFilterContext context)
    {

        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Errors = new List<string>
        {
            ResourceMessageError.ACESSO_NEGADO
        };
        dynamicResponse.StatusCode = (int)HttpStatusCode.Forbidden;

        context.Result = new UnauthorizedObjectResult(new ConcreteCreatorErrorResponse().SomeOperation(dynamicResponse));
    }
}
