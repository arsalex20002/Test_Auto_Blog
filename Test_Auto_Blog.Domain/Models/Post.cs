using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Auto_Blog.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime EditTime { get; set; }
        public long UserId { get; set; }
        public int CarId { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Avatar { get; set; }
    }
}
