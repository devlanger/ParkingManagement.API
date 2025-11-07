using CarAssignment.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure;

public interface ICarRepository
{
    Task AddAsync(Car entity);
    void Update(Car entity);
    void Delete(Car entity);
    Task<Car?> GetByIdAsync(string id);
    Task<Car?> GetParkedCarByRegistrationAsync(string vehicleRegistration);
}