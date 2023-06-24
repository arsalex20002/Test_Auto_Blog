using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_Auto_Blog.Domain.ViewModels.Car;
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
				return View(response.Data.ToList());
            }

            return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> GetCar(int id)
        {
			var response = await _carService.GetCar(id);

            if(response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data); 
            }

            return RedirectToAction("Error");
		}

        [Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
        {
			var response = await _carService.DeleteCar(id);

            if(response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return RedirectToAction("GetCars");
			}
            return RedirectToAction("Error");

        }

		[HttpGet]
		public async Task<IActionResult> Save(int id)
        {
            if(id == 0)
            {
                return View();
            }

			var response = await _carService.GetCar(id);

            if(response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
		}

        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Save(CarViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(model.Id == 0)
                {
					await _carService.CreateCar(model); 
                }
                else
                {
                    await _carService.Edit(model.Id,model);
                }
            }
			return RedirectToAction("GetCars");
		}



	}
}
