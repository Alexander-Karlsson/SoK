using Customers.Application.Customers.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Customers.Api.Exceptions;

public class CustomerExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CustomerAlreadyExistsException)
            return false;

        httpContext.Response.StatusCode =
            StatusCodes.Status409Conflict;

        await httpContext.Response.WriteAsJsonAsync(
            new { error = exception.Message },
            cancellationToken);

        return true;
    }
}