using Cookbook.Communication.Response;
using Cookbook.Exceptions;
using Cookbook.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Cookbook.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CookbookException)
            HandleCookbookException(context);
        else
            HandleOtherExceptions(context);
    }

    private void HandleCookbookException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationErrorsException:
                HandleValidationExceptions(context); break;
            case InvalidLoginException:
                HandleInvalidLoginException(context); break;
            default: break;
        }
    }

    private void HandleValidationExceptions(ExceptionContext context)
    {
        var validationErrorMessages = context.Exception as ValidationErrorsException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ErrorResponse(validationErrorMessages.ErrorMessages));
    }

    private void HandleInvalidLoginException(ExceptionContext context)
    {
        var validationErrorMessages = context.Exception as InvalidLoginException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ErrorResponse(validationErrorMessages.ErrorMessages));
    }

    private void HandleOtherExceptions(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse(ErrorMessages.UnknownError));
    }
}
