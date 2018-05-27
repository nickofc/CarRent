using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Core.Domain;

namespace CarRental.Infrastructure.Services
{
    public interface IVehicleService
    {
        Task<ICollection<Vehicle>> GetAllAsync();
        Task<Vehicle> GetByIdAsync(int vehicleId);

        Task CreateAsync(VehicleType vehicleType, int capacity, decimal pricePerDay, int seats, int numbersOfVehicles);
        Task DeleteAsync(int vehicleId);
    }
}