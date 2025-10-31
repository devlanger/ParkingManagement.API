using CarAssignment.Application.Services;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Data;
using CarAssignment.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CarAssignment.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IParkingService, ParkingService>();
        services.AddTransient<IRepository<Car>, ParkingDatabaseContext>();

    }
}