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

        private SweaterDBContext _db;
        private UserRepository _userRepository;
        public UserController(SweaterDBContext context, UserRepository userRepository)
        {
            _db = context;
            _userRepository = userRepository;
            //if (!_db.Users.Any())
            //{
            //    _db.Users.Add(new UserModel { UserId = 1, Login = "Karottensaft", Password = "12345678", Mail = "Karottensaft.Rus@gmail.com", FirstName = "Anton", LastName = "Aboba", Role = "admin" });

            //    _db.SaveChanges();
            //}
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

            _userRepository.InsertEntity(user);
            await _db.SaveChangesAsync();
            return user;
        }

        [HttpPut("{id}")]
        public async Task<UserModel> Put(UserModel user)
        {
            if (user == null)
            {
                BadRequest();
            }
            if (!_db.Users.Any(x => x.UserId == user.UserId))
            {
                NotFound();
            }

            _db.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<UserModel> Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                NotFound();
            }
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}