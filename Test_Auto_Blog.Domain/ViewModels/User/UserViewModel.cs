using Microsoft.AspNetCore.Http;
using Test_Auto_Blog.Domain.Enum;

namespace Test_Auto_Blog.Domain.ViewModels.User
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
