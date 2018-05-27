using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Domain;
using CarRental.Infrastructure.Services;
using Xunit;

namespace CarRental.Infrastructure.Tests
{
    public class RentServiceTests
    {
        [Fact]
        public async Task when_vehicle_is_already_rent_invoke_RentVehicle_should_throw_ServiceException2()
        {
            var db = Database.GetContext();
            var date = DateTime.Now;
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 1);
            var rental = Rental.Create(string.Empty, string.Empty, string.Empty, string.Empty, date);
            vehicle.Rentals.Add(rental);

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var rentService = new RentService(db);

            Assert.False(vehicle.CanRentVehicle(date));
            await Assert.ThrowsAsync<ServiceException>(async () =>
                await rentService.RentAsync(vehicle.VehicleType, string.Empty, string.Empty, string.Empty, string.Empty, date));
        }


        [Fact]
        public async Task when_vehicle_are_unavabilable_invoke_RentVehicle_should_throw_ServiceException()
        {
            var db = Database.GetContext();
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 0);

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var rentService = new RentService(db);
            var date = DateTime.Now;

            Assert.False(vehicle.CanRentVehicle(date));
            await Assert.ThrowsAsync<ServiceException>(async () =>
                await rentService.RentAsync(vehicle.VehicleType, string.Empty, string.Empty, string.Empty, string.Empty, date));
        }

        [Fact]
        public async Task when_vehicle_are_unavabilable_invoke_RentByIdAsync_should_throw_ServiceException()
        {
            var db = Database.GetContext();
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 0);

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var rentService = new RentService(db);
            var date = DateTime.Now;

            Assert.False(vehicle.CanRentVehicle(date));
            await Assert.ThrowsAsync<ServiceException>(async () =>
                await rentService.RentByIdAsync(vehicle.Id, string.Empty, string.Empty, string.Empty, string.Empty, date));
        }

        [Fact]
        public async Task when_invoke_RentByIdAsync_and_vehicle_do_not_exist_should_throw_ServiceException()
        {
            var db = Database.GetContext();

            var rentService = new RentService(db);
            var date = DateTime.Now;

            await Assert.ThrowsAsync<ServiceException>(async () =>
                await rentService.RentByIdAsync(0, string.Empty, string.Empty, string.Empty, string.Empty, date));
        }

        [Fact]
        public async Task when_invoking_RentByIdAsync_should_successfuly_rent_vehicle()
        {
            var db = Database.GetContext();
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 1);

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var date = DateTime.Now;

            Assert.Equal(1, db.Vehicles.Single()
                              .GetCountOfAvailableVehiclesToday());

            var rentService = new RentService(db);
            await rentService.RentByIdAsync(vehicle.Id, string.Empty, string.Empty, string.Empty, string.Empty, date);

            Assert.Equal(0, db.Vehicles.Single()
                              .GetCountOfAvailableVehiclesToday());

            Assert.False(vehicle.CanRentVehicleToday());
            Assert.True(vehicle.CanRentVehicle(date.AddDays(1)));
        }

        [Fact]
        public async Task when_invoking_RentAsync_should_successfuly_rent_vehicle()
        {
            var db = Database.GetContext();
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 1);

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            var date = DateTime.Now;

            Assert.Equal(1, db.Vehicles.Single()
                .GetCountOfAvailableVehiclesToday());

            var rentService = new RentService(db);
            await rentService.RentAsync(vehicle.VehicleType, string.Empty, string.Empty, string.Empty, string.Empty, date);

            Assert.Equal(0, db.Vehicles.Single()
                .GetCountOfAvailableVehiclesToday());

            Assert.False(vehicle.CanRentVehicleToday());
            Assert.True(vehicle.CanRentVehicle(date.AddDays(1)));
        }

        [Fact]
        public async Task when_invoking_GetAvailableVehiclesAsync_should_return_collection_of_available_vehicles()
        {
            var db = Database.GetContext();
            db.Vehicles.AddRange(new List<Vehicle>
            {
                Vehicle.Create(VehicleType.Car, 100, 100, 4, 5),
                Vehicle.Create(VehicleType.DeliveryTruck, 100, 100, 100, 5),
                Vehicle.Create(VehicleType.DeliveryTruck, 100, 100, 100, 0),
            });
            db.SaveChanges();

            var date = DateTime.Now;
            var rentService = new RentService(db);   
            var vehicles = await rentService.GetAvailableVehiclesAsync(date);

            // 2 = number of vehicles
            Assert.Equal(2, vehicles.Count);
            // 10 = number of available vehicles
            Assert.Equal(10, vehicles.Sum(x => x.GetCountOfAvailableVehicles(date)));
        }
    }
}
