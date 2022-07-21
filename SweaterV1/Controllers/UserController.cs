using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetListAsync()
        {
            var user = await _unitOfWork.UserRepository.GetEntityListAsync();
            return user;
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
            if (user == null)
                NotFound();
            return user;
        }

        [HttpPost]
        public async Task<UserModel> Post(UserModel user)
        {
            if (user == null)
            {
                BadRequest();
            }
            _unitOfWork.UserRepository.PostEntity(user);
            await _unitOfWork.SaveAsync();
            return  user;
        }

        [HttpPut("{id}")]
        public async Task<UserModel> Put(UserModel user)
        {
            _unitOfWork.UserRepository.UpdateEntity(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _unitOfWork.UserRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}