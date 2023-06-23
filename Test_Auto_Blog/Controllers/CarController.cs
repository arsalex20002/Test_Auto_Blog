using Microsoft.AspNetCore.Mvc;
using Test_Auto_Blog.Service.Interfaces;

namespace Test_Auto_Blog.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
           var response = await _carService.GetCars();
            if(response.Status == Domain.Enum.ErrorStatus.Success)
            {
				return View(response.Data);
            }

            return RedirectToAction("Error");
			
        }
    }
}
