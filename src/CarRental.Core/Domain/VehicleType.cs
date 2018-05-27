using System.ComponentModel;

namespace CarRental.Core.Domain
{
    public enum VehicleType
    {
        [DisplayName("Samochód dostawczy")]
        DeliveryTruck,
        [DisplayName("Limuzyna")]
        Limousine,
        [DisplayName("Samochód osobowy")]
        Car,
    }
}