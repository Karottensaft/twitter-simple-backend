using Microsoft.AspNetCore.Mvc;
using SweaterV1.Models;
using SweaterV1.Data;
using Microsoft.EntityFrameworkCore;

namespace SweaterV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        SweaterDBContext db;
        public PostsController(SweaterDBContext context)
        {
            db = context;
            if (!db.Posts.Any())
            {
                db.Posts.Add(new PostModel { PostId = 1, UserId = 1, Text = "qwerty12345678" });

                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> Get()
        {
            return await db.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> Get(int id)
        {
            PostModel post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
                return NotFound();
            return new ObjectResult(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostModel>> Post(PostModel post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostModel>> Put(PostModel post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            if (!db.Posts.Any(x => x.PostId == post.PostId))
            {
                return NotFound();
            }

            db.Update(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostModel>> Delete(int id)
        {
            PostModel post = db.Posts.FirstOrDefault(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }
    }
}