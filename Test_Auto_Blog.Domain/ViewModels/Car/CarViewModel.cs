
using Microsoft.AspNetCore.Http;

namespace Test_Auto_Blog.Domain.ViewModels.Car
{
	public class CarViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Model { get; set; }
		public DateTime DateCreate { get; set; }
		public string TypeCar { get; set; }
		public IFormFile Avatar { get; set; }
	}
}
