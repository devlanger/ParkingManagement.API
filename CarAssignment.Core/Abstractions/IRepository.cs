using CarAssignment.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure;

public interface ICarRepository
{
    Task AddAsync(Car entity);
    Task UpdateAsync(Car entity);
    void Delete(Car entity);
    Task<Car?> GetByIdAsync(string id);
    Task<Car?> GetParkedCarByRegistrationAsync(string vehicleRegistration);
}