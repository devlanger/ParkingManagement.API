using CarAssignment.Core.Data;

namespace CarAssignment.Core.Abstractions;

public interface IPaymentService
{
    void Charge(Car car, double amount);
}