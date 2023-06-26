using System.Security.Claims;
using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Car;
using Test_Auto_Blog.Domain.ViewModels.User;

namespace Test_Auto_Blog.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();

        Task<IBaseResponse<UserViewModel>> GetUser(int id);
        Task<IBaseResponse<User>> GetUserByName(string name);

        Task<IBaseResponse<User>> Create(UserViewModel car);

        Task<IBaseResponse<bool>> DeleteUser(int id);

        Task<IBaseResponse<User>> Edit(int id, UserViewModel model);
    }
}