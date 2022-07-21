using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {

        private SweaterDBContext _db;
        private PostRepository _postRepository;
        public PostController(SweaterDBContext context, PostRepository postRepository)
        {
            _db = context;
            _postRepository = postRepository;
            //if (!_db.Users.Any())
            //{
            //    _db.Users.Add(new UserModel { UserId = 1, Login = "Karottensaft", Password = "12345678", Mail = "Karottensaft.Rus@gmail.com", FirstName = "Anton", LastName = "Aboba", Role = "admin" });

            //    _db.SaveChanges();
            //}
        }

        [HttpGet]
        public async Task<IEnumerable<PostModel>> GetListAsync()
        {
            return await _postRepository.GetEntityListAsync();
        }

        [HttpGet("{id}")]
        public async Task<PostModel> GetAsync(int id)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
                NotFound();
            return post;
        }

        [HttpPost]
        public async Task<PostModel> Post(PostModel post)
        {
            if (post == null)
            {
                BadRequest();
            }

            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
            return post;
        }

        [HttpPut("{id}")]
        public async Task<PostModel> Put(PostModel post)
        {
            if (post == null)
            {
                BadRequest();
            }
            if (!_db.Posts.Any(x => x.PostId == post.PostId))
            {
                NotFound();
            }

            _db.Update(post);
            await _db.SaveChangesAsync();
            return post;
        }

        [HttpDelete("{id}")]
        public async Task<PostModel> Delete(int id)
        {
            var post = _db.Posts.FirstOrDefault(x => x.PostId == id);
            if (post == null)
            {
                NotFound();
            }
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
            return post;
        }
    }
}