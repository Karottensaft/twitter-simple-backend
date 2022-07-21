using System.Net;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(SweaterDBContext context, UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetListAsync()
        {
            return await _userRepository.GetEntityListAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetAsync(int id)
        {
            var user = await _userRepository.GetEntityByIdAsync(id);
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
            _userRepository.PostEntity(user);
            await _userRepository.SaveAsync();
            return  user;
        }

        [HttpPut("{id}")]
        public async Task<UserModel> Put(UserModel user)
        {
            _userRepository.UpdateEntity(user);
            await _userRepository.SaveAsync();
            return user;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _userRepository.DeleteEntity(id);
            await _userRepository.SaveAsync();
        }
    }
}