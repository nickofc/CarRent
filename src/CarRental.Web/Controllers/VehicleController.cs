using System.Threading.Tasks;
using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IRentService _rentService;

        public VehicleController(IVehicleService vehicleService, IRentService rentService)
        {
            _vehicleService = vehicleService;
            _rentService = rentService;
        }

        public async Task<IActionResult> Vehicles()
               => View(await _vehicleService.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> AvailableVehicles()
               => View(await _rentService.GetAvailableVehiclesTodayAsync());

    }
}