using Test_Auto_Blog.Domain.ViewModels.Car;
using Test_Auto_Blog.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data.ToList());
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = await _carService.GetCar(id);
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCar(id);
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return RedirectToAction("GetCars");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View();

            var response = await _carService.GetCar(id);
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarViewModel model)
        {
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {

                    await _carService.Create(model);
                }
                else
                {
                    await _carService.Edit(model.Id, model);
                }
                return RedirectToAction("GetCars");
            }
            return View();
        }

    }
}