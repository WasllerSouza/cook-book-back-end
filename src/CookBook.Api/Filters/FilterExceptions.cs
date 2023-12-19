using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using FactoryMethod.ConcreteCreator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CookBook.Api.Filters;

public class FilterExceptions : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CookBookException)
        {
            HandleCookBookException(context);
        }
        else
        {
           // HandleUnknownException(context);
        }
    }

    private void HandleCookBookException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorException) { HandleValidationException(context); }
        else if (context.Exception is SingInErrorException) { HandleSingInException(context); }

    }

    private void HandleValidationException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var validationErrorException = context.Exception as ValidationErrorException;

        context.Result = new ObjectResult(
                FactoryMethod(validationErrorException.ErrorsMessages.ToList(), context.HttpContext.Response.StatusCode)
           );
    }

    private void HandleSingInException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        var singInErrorException = context.Exception as SingInErrorException;

        context.Result = new ObjectResult(
                FactoryMethod(singInErrorException.ErrorsMessages.ToList(), context.HttpContext.Response.StatusCode)
           );
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var messages = new List<string>()
        {
            ResourceMessageError.ERRO_DESCONHECIDO 
        };

        context.Result = new ObjectResult(
                FactoryMethod(messages, context.HttpContext.Response.StatusCode)
           );
    }

    private dynamic FactoryMethod(List<string> errors, int statusCode)
    {
        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Errors = errors;
        dynamicResponse.StatusCode = statusCode;

        var creator = new ConcreteCreatorErrorResponse<List<string>>();

        return creator.SomeOperation(dynamicResponse);
    }
}
