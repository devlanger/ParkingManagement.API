using CarAssignment.Application.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.Workers;

public class ParkingPaymentWorker(IOptions<ParkingConfiguration> parkingPaymentConfiguration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(parkingPaymentConfiguration.Value.PaymentIntervalInSeconds), stoppingToken);
        }
    }
}