using CookBook.Communication.ConcreteBuilder;
using CookBook.Communication.Director;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
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
        if(context.Exception is CookBookException)
        {
            HandleCookBookException(context);
        }
        else
        {
            HandleUnknownException(context);
        }
    }

    private void HandleCookBookException(ExceptionContext context)
    {
        if(context.Exception is ValidationErrorException)
        {
            HandleValidationException(context);
        }

    }

    private void HandleValidationException(ExceptionContext context) 
    {
        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;

        var validationErrorException = context.Exception as ValidationErrorException;

        var genericResponse = new GenericResponseDirector<List<string>>(new GenericResponseError<List<string>>());
        genericResponse.CreateGenericResponse(validationErrorException.ErrorsMessages.ToList(), context.HttpContext.Response.StatusCode);
        
        context.Result = new ObjectResult(genericResponse.GetGenericResponse());
    }
    
    private void HandleUnknownException(ExceptionContext context) 
    {
        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

        var messages = new List<string>();
        messages.Add(ResourceMessageError.ERRO_DESCONHECIDO);

        var genericResponse = new GenericResponseDirector<List<string>>(new GenericResponseError<List<string>>());
        genericResponse.CreateGenericResponse(messages, context.HttpContext.Response.StatusCode);
        
        context.Result = new ObjectResult(genericResponse.GetGenericResponse());
    }
}
