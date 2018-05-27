using CarRental.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Database
{
    public class Context : DbContext
    {
        protected Context()
        {
        }

        public Context(DbContextOptions options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        public virtual DbSet<Vehicle> Vehicles { get; protected set; }
        public virtual DbSet<Rental> Rentals { get; protected set; }
    }
}
