using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostModel>> GetListAsync()
        {
            var post = await _postService.GerListOfEntities();
            return post;
        }

        [HttpGet("{id}")]
        public async Task<PostModel> GetAsync(int id)
        {
            var post = await _postService.GetEntity(id);
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
            await _postService.CreateEntity(post);
            return post;
        }

        [HttpPut("{id}")]
        public async Task<PostModel> Put(PostModel post)
        {
            await _postService.UpdateEntity(post);
            return post;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _postService.DeleteEntity(id);
        }
    }
}