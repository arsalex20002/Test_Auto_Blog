using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.DAL.Repositories;
using Test_Auto_Blog.Domain.Enum;
using Test_Auto_Blog.Domain.Helpers;
using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Car;
using Test_Auto_Blog.Domain.ViewModels.User;
using Test_Auto_Blog.Service.Interfaces;

namespace Test_Auto_Blog.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMainRepository<User> _UserRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IMainRepository<User> UserRepository, ILogger<UserService> logger)
        {
            _UserRepository = UserRepository;
            _logger = logger;
        }
        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _UserRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                await _UserRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _UserRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<User>> GetUserByName(string name)
        {
            try
            {
                var user = await _UserRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "user not found",
                        Status = ErrorStatus.UserNotFound
                    };
                }

                return new BaseResponse<User>()
                {
                    Data = user,
                    Status = ErrorStatus.Success,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetCarByName] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        public Task<IBaseResponse<User>> Create(UserViewModel car)
        {
            throw new NotImplementedException();
        }

        

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _UserRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "user not found",
                        Status = ErrorStatus.UserNotFound,
                        Data = false
                    };
                }

                if (user.Role == Role.Admin)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Вы не можете удалить аккаунт с правами равными вашим",
                        Status = ErrorStatus.InternalServerError,
                        Data = false
                    };
                }

                await _UserRepository.Delete(user);

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

        public Task<IBaseResponse<User>> Edit(int id, UserViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<UserViewModel>> GetUser(int id)
        {
            try
            {
                var user = await _UserRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = "User не найден",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                var data = new UserViewModel()
                {
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    Role = user.Role.ToString()
                };

                return new BaseResponse<UserViewModel>()
                {
                    Status = ErrorStatus.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var user = _UserRepository.GetAll();
                if (!user.Any())
                {
                    return new BaseResponse<IEnumerable<User>>()
                    {
                        Description = "Найдено 0 элементов",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<IEnumerable<User>>()
                {
                    Data = user,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
    }
}
