using System;
using System.Threading.Tasks;
using CarRental.Core.Domain;
using CarRental.Infrastructure.Services;
using CarRental.Web.Controllers;
using CarRental.Web.Models;
using Moq;
using Xunit;

namespace CarRental.Web.Tests
{
    public class RentControllerTests
    {
        [Fact]
        public async Task when_invoking_index_should_invoke_RentAsync_on_RentService()
        {
            var mockRentService = new Mock<IRentService>();   
            var controller = new RentController(mockRentService.Object);
            var model = new RentViewModel
            {
                DateTime = DateTime.Now,
                FistName = "Developer",
                Street = "asdf",
                City = "asdf",
                PhoneNumber = "534534534",
                VehicleType = VehicleType.Car
            };
            await controller.Index(model);
            mockRentService.Verify(x => x.RentAsync(It.IsAny<VehicleType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
        }
    }
}

