using CarAssignment.Application.Services;
using CarAssignment.Core.Abstractions;
using CarAssignment.Infrastructure;
using CarAssignment.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace CarAssignment.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IParkingService, ParkingService>();
        services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
    }
}