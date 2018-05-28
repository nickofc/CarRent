using System;
using CarRental.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Tests
{
    public static class Database
    {
        public static Context BuildContextForTest(string name = default(string))
        {
            var options = new DbContextOptionsBuilder();
            options.UseInMemoryDatabase(name ?? Guid.NewGuid().ToString()); 
            return new Context(options.Options);
        }
    }
}
