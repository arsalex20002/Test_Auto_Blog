using Test_Auto_Blog.Domain.Enum;

namespace Test_Auto_Blog.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
