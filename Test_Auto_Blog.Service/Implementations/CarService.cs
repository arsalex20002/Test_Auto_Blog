using Microsoft.EntityFrameworkCore;
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
        private readonly IMainRepository<Car> _carRepository;

        public CarService(IMainRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<CarViewModel>> GetCar(int id)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    return new BaseResponse<CarViewModel>()
                    {
                        Description = "Машины не найден",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                var data = new CarViewModel()
                {
                    Name = car.Name,
                    DateCreate = car.DateCreate,
                    Description = car.Description,
                    TypeCar = car.TypeCar.ToString(),
                };

                return new BaseResponse<CarViewModel>()
                {
                    Status = ErrorStatus.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> Create(CarViewModel model)
        {
            try
            {
                var car = new Car()
                {
                    Name = model.Name,
                    Description = model.Description,
                    DateCreate = DateTime.Now,
                    TypeCar = (TypeCar)Enum.Parse(typeof(TypeCar), model.TypeCar),
                };
                await _carRepository.Create(car);

                return new BaseResponse<Car>()
                {
                    Status = ErrorStatus.Success,
                    Data = car
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[Create] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCar(int id)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Car not found",
                        Status = ErrorStatus.CarNotFound,
                        Data = false
                    };
                }

                await _carRepository.Delete(car);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
                if (car == null)
                {
                    return new BaseResponse<Car>()
                    {
                        Description = "Car not found",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<Car>()
                {
                    Data = car,
                    Status = ErrorStatus.Success,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCarByName] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel model)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    return new BaseResponse<Car>()
                    {
                        Description = "Car not found",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                car.Description = model.Description;
                car.DateCreate = model.DateCreate;
                car.Name = model.Name;

                await _carRepository.Update(car);


                return new BaseResponse<Car>()
                {
                    Data = car,
                    Status = ErrorStatus.Success,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            try
            {
                var cars = _carRepository.GetAll();
                if (!cars.Any())
                {
                    return new BaseResponse<IEnumerable<Car>>()
                    {
                        Description = "Найдено 0 элементов",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<IEnumerable<Car>>()
                {
                    Data = cars,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
    }
}