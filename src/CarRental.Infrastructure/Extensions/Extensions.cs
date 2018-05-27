using CarRental.Core.Domain;
using CarRental.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static T Resolve<T>(this IServiceCollection services)
        {
            return services.BuildServiceProvider().GetRequiredService<T>();
        }

        public static void Seed(this Context context)
        {
            context.Vehicles.Add(Vehicle.Create(VehicleType.DeliveryTruck, 1000, 200, 2, 2));
            context.Vehicles.Add(Vehicle.Create(VehicleType.Car, 100, 100, 4, 4));
            context.Vehicles.Add(Vehicle.Create(VehicleType.Limousine, 250, 500, 8, 2));
            context.SaveChanges();
        }
    }
}