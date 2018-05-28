using System;

namespace CarRental.Core.Domain
{
    public class Rental
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }

        // todo extract class?
        public string FistName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        protected Rental()
        {

        }

        public Rental(string fistName, string phoneNumber, string city, string street, DateTime date)
        {
            Date = date;
            FistName = fistName;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }
    }
}
