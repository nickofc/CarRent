using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Core.Domain
{
    public class Vehicle
    {
        private readonly HashSet<Rental> _rentals = new HashSet<Rental>();

        public int Id { get; private set; }
        public VehicleType VehicleType { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerDay { get; set; }
        public int Seats { get; set; }
        public int NumberOfVehicles { get; set; }

        public virtual ICollection<Rental> Rentals => _rentals;

        private Vehicle()
        {

        }


        public int GetCountOfAvailableVehiclesToday()
            => GetCountOfAvailableVehicles(DateTime.Now);

        public bool CanRentVehicleToday()
            => CanRentVehicle(DateTime.Now);

        public int GetCountOfAvailableVehicles(DateTime date)
            => NumberOfVehicles - Rentals.Count(x => x.Date.Year == date.Year && x.Date.Month == date.Month && date.Day == x.Date.Day);

        public bool CanRentVehicle(DateTime date)
            => GetCountOfAvailableVehicles(date) > 0;

        public static Vehicle Create(VehicleType vehicleType, int capacity, decimal pricePerDay, int seats, int numbersOfVehicles)
        {
            if (capacity < 0)
                throw new DomainException($"{nameof(capacity)} is out of range!");

            if (pricePerDay < 0)
                throw new DomainException($"{nameof(pricePerDay)} is out of range!");

            if (seats < 0)
                throw new DomainException($"{nameof(seats)} is out of range!");

            if (numbersOfVehicles < 0)
                throw new DomainException($"{nameof(numbersOfVehicles)} is out of range!");

            return new Vehicle
            {
                VehicleType = vehicleType,
                Capacity = capacity,
                PricePerDay = pricePerDay,
                Seats = seats,
                NumberOfVehicles = numbersOfVehicles,
            };
        }
    }
}
