using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        [HttpGet]
        public async Task<IEnumerable<PostModel>> GetListAsync()
        {
            var post = await _unitOfWork.PostRepository.GetEntityListAsync();
            return post;
        }

        [HttpGet("{id}")]
        public async Task<PostModel> GetAsync(int id)
        {
            var post = await _unitOfWork.PostRepository.GetEntityByIdAsync(id);
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
            _unitOfWork.PostRepository.PostEntity(post);
            await _unitOfWork.SaveAsync();
            return post;
        }

        [HttpPut("{id}")]
        public async Task<PostModel> Put(PostModel post)
        {
            _unitOfWork.PostRepository.UpdateEntity(post);
            await _unitOfWork.SaveAsync();
            return post;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _unitOfWork.PostRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}