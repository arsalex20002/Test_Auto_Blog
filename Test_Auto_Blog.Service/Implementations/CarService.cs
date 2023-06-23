using System.Xml.Linq;
using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.Domain.Enum;
using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Car;
using Test_Auto_Blog.Service.Interfaces;

namespace Test_Auto_Blog.Service.Implementations
{
	public class CarService : ICarService
	{
		private readonly ICarRepository _carRepository;
		public CarService(ICarRepository carRepository) 
		{
			_carRepository = carRepository;
		}

		public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
		{
			var sqlResponse = new BaseResponse<CarViewModel>();

			try
			{
				var car = new Car()
				{
					Name = carViewModel.Name,
					Description = carViewModel.Description,
					DateCreate = DateTime.Now,
					Model = carViewModel.Model,
					TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)

				};

				await _carRepository.Create(car);
				sqlResponse.Status = ErrorStatus.Success;

				return sqlResponse;
			}
			catch (Exception ex)
			{

				return new BaseResponse<CarViewModel>()
				{
					Description = $"[GetCar] : {ex.Message}",
					Status = ErrorStatus.CarNotFound,
				};
			}
		}

		public async Task<IBaseResponse<bool>> DeleteCar(int id)
		{
			var sqlResponse = new BaseResponse<bool>();
			try
			{
				var car = await _carRepository.GetById(id);
				if (car == null)
				{
					sqlResponse.Description = "Машина не найдена";
					sqlResponse.Status = ErrorStatus.CarNotFound;
					sqlResponse.Data = false;
					return sqlResponse;
				}

				await _carRepository.Delete(car);
				sqlResponse.Data = true;
				return sqlResponse;


			}
			catch (Exception ex)
			{

				return new BaseResponse<bool>()
				{
					Description = $"[GetCar] : {ex.Message}",
					Status = ErrorStatus.CarNotFound,
				};
			}
		}

		public async Task<IBaseResponse<Car>> GetCarByName(string name)
		{
			var sqlResponse = new BaseResponse<Car>();
			try
			{
				var car = await _carRepository.GetByName(name);
				if (car == null)
				{
					sqlResponse.Description = "Машина не найдена";
					sqlResponse.Status = ErrorStatus.CarNotFound;
					return sqlResponse;
				}
				sqlResponse.Data = car;
				return sqlResponse;
			}
			catch (Exception ex)
			{

				return new BaseResponse<Car>()
				{
					Description = $"[GetCar] : {ex.Message}",
					Status = ErrorStatus.CarNotFound,
				};
			}
		}

		public async Task<IBaseResponse<Car>> GetCar(int id)
		{
			var sqlResponse = new BaseResponse<Car>();
			try
			{
				var car = await _carRepository.GetById(id);
				if(car == null)
				{
					sqlResponse.Description = "Машина не найдена";
					sqlResponse.Status = ErrorStatus.CarNotFound;
					return sqlResponse;
				}
				sqlResponse.Data = car;
				return sqlResponse;
			}
			catch (Exception ex)
			{

				return new BaseResponse<Car>()
				{
					Description = $"[GetCar] : {ex.Message}",
					Status = ErrorStatus.CarNotFound,
				};
			}
		}

		public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
		{
			var sqlResponse = new BaseResponse<IEnumerable<Car>>();
			try
			{
				var cars = await _carRepository.Select();
				if(cars.Count == 0)
				{
					sqlResponse.Description = "Элементы не были найдены";
					sqlResponse.Status = ErrorStatus.Success;
					return sqlResponse;
				}

				sqlResponse.Data = cars;
				sqlResponse.Status = ErrorStatus.Success;

				return sqlResponse;
			}
			catch (Exception ex)
			{

				return new BaseResponse<IEnumerable<Car>>()
				{
					Description = $"[GetCar] : {ex.Message}",
					Status = ErrorStatus.CarNotFound,
				};
			}
		}
	}
}
