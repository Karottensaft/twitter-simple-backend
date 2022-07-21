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
        private readonly PostRepository _postRepository;
        public PostController(SweaterDBContext context, PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PostModel>> GetListAsync()
        {
            return await _postRepository.GetEntityListAsync();
        }

        [HttpGet("{id}")]
        public async Task<PostModel> GetAsync(int id)
        {
            var post = await _postRepository.GetEntityByIdAsync(id);
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
            _postRepository.PostEntity(post);
            await _postRepository.SaveAsync();
            return post;
        }

        [HttpPut("{id}")]
        public async Task<PostModel> Put(PostModel post)
        {
            _postRepository.UpdateEntity(post);
            await _postRepository.SaveAsync();
            return post;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _postRepository.DeleteEntity(id);
            await _postRepository.SaveAsync();
        }
    }
}