using Microsoft.EntityFrameworkCore;
using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.DAL.Repositories;
using Test_Auto_Blog.Domain.Enum;
using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Car;
using Test_Auto_Blog.Domain.ViewModels.Post;
using Test_Auto_Blog.Service.Interfaces;

namespace Test_Auto_Blog.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly IMainRepository<Post> _postRepository;
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        public PostService(IMainRepository<Post> postRepository, IUserService userService, ICarService carService)
        {
            _postRepository = postRepository;
            _userService = userService;
            _carService = carService;
        }
        public async Task<IBaseResponse<Post>> Create(PostViewModel model,string UserName)
        {
            try
            {
                var responce_car = await _carService.GetCarByName(model.Car);
                var responce_user = await _userService.GetUserByName(UserName);
                //преобразуем фото в массив данных
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }

                var post = new Post()
                {
                    Name = model.Name,
                    Description = model.Description,
                    DateCreate = DateTime.Now.ToUniversalTime(),
                    EditTime = DateTime.Now,
                    CarId = responce_car.Data.Id,
                    UserId = responce_user.Data.Id,
                    IsPublic = model.IsPublic,
                    Avatar = imageData
                
                };

                await _postRepository.Create(post);

                return new BaseResponse<Post>()
                {
                    Status = ErrorStatus.Success,
                    Data = post
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Create] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePost(int id)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (post == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "post not found",
                        Status = ErrorStatus.PostNotFound,
                        Data = false
                    };
                }

                await _postRepository.Delete(post);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePost] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Post>> Edit(int id, PostViewModel model)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var car = await _carService.GetCarByName(model.Car);
                if (post == null)
                {
                    return new BaseResponse<Post>()
                    {
                        Description = "Car not found",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
                post.Avatar = imageData;
                post.Name = model.Name;
                post.CarId = car.Data.Id;
                post.IsPublic = model.IsPublic;
                post.Description = model.Description;
                post.EditTime = DateTime.Now;
                await _postRepository.Update(post);


                return new BaseResponse<Post>()
                {
                    Data = post,
                    Status = ErrorStatus.Success,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PostViewModel>> GetPost(int id)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (post == null)
                {
                    return new BaseResponse<PostViewModel>()
                    {
                        Description = "Пост не найден",
                        Status = ErrorStatus.PostNotFound
                    };
                }
                var car = await _carService.GetCar(post.CarId);

                var data = new PostViewModel()
                {
                    Name = post.Name,
                    Description = post.Description,
                    DateCreate = post.DateCreate,
                    EditTime = post.EditTime,
                    Car = car.Data.Name,
                    IsPublic = post.IsPublic,
                    Image = post.Avatar
                };

                return new BaseResponse<PostViewModel>()
                {
                    Status = ErrorStatus.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostViewModel>()
                {
                    Description = $"[GetPost] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Post>> GetPostById(int id)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (post == null)
                {
                    return new BaseResponse<Post>()
                    {
                        Description = "Пост не найден",
                        Status = ErrorStatus.PostNotFound
                    };
                }

                return new BaseResponse<Post>()
                {
                    Status = ErrorStatus.Success,
                    Data = post
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[GetPost] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Post>>> GetPostsByName(string name)
        {
            try
            {
                var user_id = await _userService.GetUserByName(name);
                var posts = _postRepository.GetAll().Where(x => x.UserId == user_id.Data.Id);
                if (!posts.Any())
                {
                    return new BaseResponse<IEnumerable<Post>>()
                    {
                        Description = "Найдено 0 элементов",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<IEnumerable<Post>>()
                {
                    Data = posts,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Post>>()
                {
                    Description = $"[GetPosts] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<Post>>> GetPosts(string typeCar = null, string Name = null, int PostDate = 0, int CarDate = 0)
        {
            try
            {
                var posts = _postRepository.GetAll().Where(x => x.IsPublic == true);
                if (!posts.Any())
                {
                    return new BaseResponse<IEnumerable<Post>>()
                    {
                        Description = "Найдено 0 элементов",
                        Status = ErrorStatus.CarNotFound
                    };
                }
                if(PostDate != 0)
                {
                    posts = posts.Where(x => x.DateCreate.Year == PostDate);
                }
                if (typeCar != null && typeCar != "all")
                {
                    var carTypeCheck = await _carService.GetCars();
                    foreach (var post in posts)
                    {
                        if (carTypeCheck.Data.ElementAt(post.CarId - 1).TypeCar.ToString() != typeCar)
                        {
                            posts = posts.Where(x => x.Id != post.Id);
                        }
                    }
                }
                if (CarDate != 0)
                {
                    var carDateCheck = await _carService.GetCars();
                    foreach (var post in posts)
                    {
                        if (carDateCheck.Data.ElementAt(post.CarId - 1).DateCreate.Year != CarDate)
                        {
                            posts = posts.Where(x => x.Id != post.Id);
                        }
                    }
                }
                if(Name != null && Name != "0")
                {
                    var carNameCheck = await _carService.GetCars();
                    foreach (var post in posts)
                    {
                        if (carNameCheck.Data.ElementAt(post.CarId - 1).Name != Name)
                        {
                            posts = posts.Where(x => x.Id != post.Id);
                        }
                    }
                }


                return new BaseResponse<IEnumerable<Post>>()
                {
                    Data = posts,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Post>>()
                {
                    Description = $"[GetPosts] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
    }
}
