using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Test_Auto_Blog.Domain.ViewModels.Post
{
    public class PostViewModel
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public DateTime EditTime { get; set; }
        [Required]
        public string Car { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public IFormFile Avatar { get; set; }
        public byte[] Image { get; set; }
    }
}
