using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Car;

namespace Test_Auto_Blog.Service.Interfaces
{
	public interface ICarService
	{
		Task<IBaseResponse<IEnumerable<Car>>> GetCars();
		public Task<IBaseResponse<Car>> GetCarByName(string name);
		public Task<IBaseResponse<Car>> GetCar(int id);
		public Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel);
		public Task<IBaseResponse<bool>> DeleteCar(int id);
	}
}
