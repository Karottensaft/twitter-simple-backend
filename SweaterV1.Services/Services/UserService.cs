using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using System.Collections.Generic;
using SweaterV1.Services.Services;
using Microsoft.AspNetCore.Mvc;


namespace SweaterV1.Services.Services
{
    public class UserService : IService<UserModel>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public UserService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserModel>> GerListOfEntities()
        {
            return await _unitOfWork.UserRepository.GetEntityListAsync();
        }

        public async Task<UserModel> GetEntity(int id)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
            return user;
        }

        public async Task CreateEntity(UserModel user)
        {
            _unitOfWork.UserRepository.PostEntity(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEntity(UserModel user)
        {

                _unitOfWork.UserRepository.UpdateEntity(user);
                await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEntity(int id)
        {
            _unitOfWork.UserRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}