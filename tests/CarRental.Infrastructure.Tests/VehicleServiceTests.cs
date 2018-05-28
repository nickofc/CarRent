using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Domain;
using CarRental.Infrastructure.Services;
using Xunit;

namespace CarRental.Infrastructure.Tests
{
    public class VehicleServiceTests
    {
        private static Vehicle CreateVehicle()
        {
            return new Vehicle(VehicleType.Car, 1, 1, 1, 1);
        }

        [Fact]
        public async Task when_invoking_CreateAsync_should_create_vehicle_and_add_to_db()
        {
            var db = Database.BuildContextForTest();
            var vehicle = CreateVehicle();

            var vehicleService = new VehicleService(db);
            await vehicleService.CreateAsync(vehicle.VehicleType, vehicle.Capacity, vehicle.PricePerDay, vehicle.Seats,
                vehicle.NumberOfVehicles);

            Assert.Equal(1, db.Vehicles.Count());
        }

        [Fact]
        public async Task when_invoking_DeleteAsync_should_delete_vehicle_from_db()
        {
            var db = Database.BuildContextForTest();
            var vehicle = CreateVehicle();
            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var vehicleService = new VehicleService(db);
            await vehicleService.DeleteAsync(vehicle.Id);

            Assert.False(db.Vehicles.Any());
        }

        [Fact]
        public async Task when_invoking_DeleteAsyc_and_vehicle_not_exist_should_throw_ServiceException()
        {
            var db = Database.BuildContextForTest();
            var vehicleService = new VehicleService(db);
            await Assert.ThrowsAsync<ServiceException>(async () =>
                await vehicleService.DeleteAsync(0));
        }

        [Fact]
        public async Task when_invoking_GetByIdAsync_should_return_vehicle_from_db()
        {
            var db = Database.BuildContextForTest();
            var vehicle = CreateVehicle();

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var service = new VehicleService(db);
            Assert.Equal(vehicle, await service.GetByIdAsync(vehicle.Id));
        }
    }
}
