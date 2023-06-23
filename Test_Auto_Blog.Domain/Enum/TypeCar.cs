using System.ComponentModel.DataAnnotations;

namespace Test_Auto_Blog.Domain.Enum
{
    public enum TypeCar
    {
        [Display(Name = "Легковой автомобиль")]
        PassengerCar = 0,
        [Display(Name = "Грузовой автомобиль")]
        Truck = 1,
        [Display(Name = "Автобус")]
        Bus = 2,
        [Display(Name = "Седан")]
        Sedan = 3,
        [Display(Name = "Спортвная машина")]
        SportCar = 4

    }
}
