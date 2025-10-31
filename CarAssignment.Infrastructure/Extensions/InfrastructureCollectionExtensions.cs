using Microsoft.Extensions.DependencyInjection;

namespace CarAssignment.Infrastructure.Extensions;

public static class InfrastructureCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ParkingDatabaseContext>();
    }
}