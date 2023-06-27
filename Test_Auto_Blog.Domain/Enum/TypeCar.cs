using System.ComponentModel.DataAnnotations;

namespace Test_Auto_Blog.Domain.Enum
{
    public enum TypeCar
    {
        [Display(Name = "Легковой автомобиль")]
        lightCar = 0,
        [Display(Name = "Грузовой автомобиль")]
        truck = 1,
        [Display(Name = "Автобус")]
        bus = 2,
        [Display(Name = "Седан")]
        sedan = 3,
        [Display(Name = "Спортивная машина")]
        sportCar = 4

    }
}
