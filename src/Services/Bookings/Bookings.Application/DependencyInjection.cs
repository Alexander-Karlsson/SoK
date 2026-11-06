using Bookings.Application.Bookings.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bookings.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<BookingAppService>();

        return services;
    }
}
