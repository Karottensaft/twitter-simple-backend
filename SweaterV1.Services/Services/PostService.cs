using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using System.Collections.Generic;
using SweaterV1.Services.Services;
using Microsoft.AspNetCore.Mvc;


namespace SweaterV1.Services.Services
{
    public class PostService : IService<PostModel>
    {
        private UnitOfWork _unitOfWork;

        public PostService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PostModel>> GerListOfEntities()
        {
            return await _unitOfWork.PostRepository.GetEntityListAsync();
        }

        public async Task<PostModel> GetEntity(int id)
        {
            var post = await _unitOfWork.PostRepository.GetEntityByIdAsync(id);
            return post;
        }

        public async Task CreateEntity(PostModel post)
        {
            _unitOfWork.PostRepository.PostEntity(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEntity(PostModel post)
        {

            _unitOfWork.PostRepository.UpdateEntity(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEntity(int id)
        {
            _unitOfWork.PostRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}