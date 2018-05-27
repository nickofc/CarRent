using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Domain;
using CarRental.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly Context _context;

        public VehicleService(Context context)
        {
            _context = context;
        }

        public async Task<ICollection<Vehicle>> GetAllAsync()
            => await _context.Vehicles.ToArrayAsync();

        public async Task<Vehicle> GetByIdAsync(int vehicleId)
            => (await _context.Vehicles.Include(x => x.Rentals).ToArrayAsync()).SingleOrDefault(x => x.Id == vehicleId);

        public async Task CreateAsync(VehicleType vehicleType, int capacity, decimal pricePerDay, int seats, int numbersOfVehicles)
        {
            var entity = Vehicle.Create(vehicleType, capacity, pricePerDay, seats, numbersOfVehicles);

            await _context.Vehicles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int vehicleId)
        {
            var entity = await GetByIdAsync(vehicleId);
            if (entity == null)
                throw new ServiceException("Vehicle not found.");

            _context.Vehicles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
