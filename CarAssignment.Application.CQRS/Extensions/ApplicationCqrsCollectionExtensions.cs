using CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CarAssignment.Application.CQRS.Extensions;

public static class ApplicationCqrsCollectionExtensions
{
    public static void AddApplicationCqrsServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AllocateVehicleCommandValidator).Assembly);
    }
}