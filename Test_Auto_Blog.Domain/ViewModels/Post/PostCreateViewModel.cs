using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.ViewModels.Car;

namespace Test_Auto_Blog.Domain.ViewModels.Post
{
    public class PostCreateViewModel
    {
        public PostViewModel PostViewModel { get; set; }
        public List<string> CarNames { get; set; }
    }
}
