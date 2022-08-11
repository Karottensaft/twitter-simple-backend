using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class PostRepository : IRepository<PostModel>
    {
        private readonly SweaterDbContext _db;

        public PostRepository(SweaterDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PostModel>> GetEntityListAsync()
        {
            return await _db.Posts.ToListAsync();
        }

        public async Task<IEnumerable<PostModel>> GetEntityListAsyncByUserId(int userId)
        {
            return  await _db.Posts.Where(x => x.UserId == userId).ToListAsync();
            
        }
        public async Task<PostModel> GetEntityByIdAsync(int postId)
        {
            var post =  await _db.Posts.SingleOrDefaultAsync(x => x.PostId == postId);
            if (post == null)
            {
                throw new Exception("Post doesn't exist.");
            }
            return post;
        }

        public void PostEntity(PostModel post)
        {
            _db.Posts.Add(post);
        }

        public void DeleteEntity(int postId)
        {
            var post = _db.Posts.Find(postId);
            if (post == null)
            {
                throw new Exception("Post doesn't exist.");
            }
            _db.Posts.Remove(post);
        }

        public void UpdateEntity(PostModel post)
        {
            _db.Entry(post).State = EntityState.Modified;
        }


        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
