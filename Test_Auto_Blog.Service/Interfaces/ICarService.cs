using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Car;

namespace Test_Auto_Blog.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();

        Task<IBaseResponse<CarViewModel>> GetCar(int id);

        Task<IBaseResponse<Car>> Create(CarViewModel car);

        Task<IBaseResponse<bool>> DeleteCar(int id);

        Task<IBaseResponse<Car>> GetCarByName(string name);

        Task<IBaseResponse<Car>> Edit(int id, CarViewModel model);
    }
}