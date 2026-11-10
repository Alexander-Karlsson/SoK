using Customers.Application.Customers.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Customers.Api.Exceptions;

public class CustomerExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            CustomerAlreadyExistsException => StatusCodes.Status409Conflict,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => 0
        };

        if (statusCode == 0)
            return false;
        
        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsJsonAsync(
            new { error = exception.Message },
            cancellationToken);

        return true;
    }
}