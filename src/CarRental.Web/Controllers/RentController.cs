using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Domain;
using CarRental.Infrastructure;
using CarRental.Infrastructure.Services;
using CarRental.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class RentController : Controller
    {
        private readonly IRentService _rentService;

        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        public IActionResult Index() => View();
        public IActionResult Failed() => View();
        public IActionResult Success() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RentViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);
            
            var date = model.DateTime.GetValueOrDefault();

            try
            {
                await _rentService.RentAsync(model.VehicleType, model.FistName, model.PhoneNumber, model.City, model.Street, date);
                return View("Success");
            }
            catch (ServiceException)
            {
                return View("Failed");
            } 
        }
    }
}