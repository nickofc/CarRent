using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Core.Domain;

namespace CarRental.Infrastructure.Services
{
    public interface IRentService
    {
        Task<ICollection<Vehicle>> GetAvailableVehiclesTodayAsync();
        Task<ICollection<Vehicle>> GetAvailableVehiclesAsync(DateTime date);
        Task RentAsync(VehicleType vehicleType, string firstName, string phoneNumber, string city, string street, DateTime date);
        Task RentByIdAsync(int vehicleId, string firstName, string phoneNumber, string city, string street, DateTime date);
    }
}