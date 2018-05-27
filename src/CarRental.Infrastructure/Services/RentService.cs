using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Domain;
using CarRental.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services
{
    public class RentService : IRentService
    {
        private readonly Context _context;

        public RentService(Context context)
        {
            _context = context;
        }

        public async Task<ICollection<Vehicle>> GetAvailableVehiclesTodayAsync() => await GetAvailableVehiclesAsync(DateTime.Now);

        public async Task<ICollection<Vehicle>> GetAvailableVehiclesAsync(DateTime date)
        {
            var vehicles =  await _context.Vehicles.Include(x => x.Rentals).ToArrayAsync();
            return vehicles.Where(x => x.CanRentVehicle(date)).ToArray();
        }

        public async Task RentAsync(VehicleType vehicleType, string firstName, string phoneNumber, string city, string street, DateTime date)
        {
            var vehicles = (await GetAvailableVehiclesAsync(date))
                                 .Where(x => x.VehicleType == vehicleType).ToArray();

            if (vehicles.Any() == false)
                throw new ServiceException("No vehicles available.");

            var vehicle = vehicles.First();
            var rental = Rental.Create(firstName, phoneNumber, city, street, date);

            vehicle.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task RentByIdAsync(int vehicleId, string firstName, string phoneNumber, string city, string street, DateTime date)
        {
            var vehicle = (await GetAvailableVehiclesAsync(date))
                                .SingleOrDefault(x => x.Id == vehicleId);

            if (vehicle == null)
                throw new ServiceException("No vehicle available.");

            var rental = Rental.Create(firstName, phoneNumber, city, street, date);
            vehicle.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }
    }
}
