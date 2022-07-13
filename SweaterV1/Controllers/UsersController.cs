using Microsoft.AspNetCore.Mvc;
using SweaterV1.Models;
using SweaterV1.Data;
using Microsoft.EntityFrameworkCore;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        SweaterDBContext db;
        public UsersController(SweaterDBContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new UserModel {  UserId = 1, Login = "Karottensaft", Password = "12345678", Mail = "Karottensaft.Rus@gmail.com", FirstName = "Anton", LastName = "Aboba",  Role = "admin"});
                
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            UserModel user = await db.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.UserId == user.UserId))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            UserModel user = db.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}