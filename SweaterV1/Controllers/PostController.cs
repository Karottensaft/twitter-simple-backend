using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("posts")]
        public async Task<IEnumerable<PostModel>> GetListAsync()
        {
            var post = await _postService.GerListOfEntities();
            return post;
        }
        [Authorize]
        [HttpGet("user/{userId}/posts")]
        public async Task<IEnumerable<PostModelInformationDto>> GetListAsyncByUser(int userId)
        {
            var post = await _postService.GerListOfEntitiesByUser(userId);
            return post;
        }

        [HttpGet("post/{postId}")]
        public async Task<PostModelInformationDto> GetAsync(int id)
        {
            var user = await _postService.GetEntity(id);
            return user!;
        }

        [HttpPost("user/{userId}/post-creator")]
        public async Task<PostModelCreationDto> Post(PostModelCreationDto post)
        {
            if (post == null) BadRequest();
            await _postService.CreateEntity(post!);
            return post!;
        }

        [HttpPut("user/{userId}/post-settings")]
        public async Task<PostModelChangeDto> Put(PostModelChangeDto post, int id)
        {
            await _postService.UpdateEntity(post, id);
            return post;
        }

        [HttpDelete("post/{userId}/deleter")]
        public async Task Delete(int id)
        {
            await _postService.DeleteEntity(id);
        }
    }
}