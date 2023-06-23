using Test_Auto_Blog.Domain.Enum;

namespace Test_Auto_Blog.Domain.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Model { get; set; }

        public DateTime DateCreate { get; set; }

        public TypeCar TypeCar { get; set; }
        
    }

}
