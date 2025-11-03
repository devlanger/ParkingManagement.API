using CarAssignment.Core.Data;

namespace CarAssignment.Core.Abstractions;

public interface IPaymentService
{
    Task<double> ChargeCar(Car carToCharge);
}