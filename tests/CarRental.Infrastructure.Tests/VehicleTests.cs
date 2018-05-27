using System;
using CarRental.Core.Domain;
using Xunit;

namespace CarRental.Infrastructure.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void when_vehicle_are_unavabilable_invoke_CanRentVehicle_should_return_false()
        {
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 0);
            Assert.False(vehicle.CanRentVehicle(DateTime.Now));
        }

        [Fact]
        public void when_vehicle_is_avabilable_invoke_CanRentVehicle_should_return_true()
        {   
            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, 1);
            var date = DateTime.Now;
            
            Assert.True(vehicle.CanRentVehicle(date));
            vehicle.Rentals.Add(Rental.Create(string.Empty, string.Empty, string.Empty, string.Empty, date));
            Assert.False(vehicle.CanRentVehicle(date));
        }

        [Fact]
        public void should_have_equal_numberOfCars()
        {
            const int numberOfVehicles = 1;

            var vehicle = Vehicle.Create(VehicleType.Car, 100, 100, 4, numberOfVehicles);
            Assert.Equal(numberOfVehicles, vehicle.GetCountOfAvailableVehiclesToday());
        }
    }
}