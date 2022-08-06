using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class LikeRepository : IRepository<LikeModel>, IDisposable
    {
        private readonly SweaterDbContext _db;

        public LikeRepository(SweaterDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<LikeModel>> GetEntityListAsync()
        {
            return await _db.Likes.ToListAsync();
        }

        public async Task<IEnumerable<LikeModel>> GetEntityListAsyncByPostId(int postId)
        {
            return await _db.Likes.Where(x => x.PostId == postId).ToListAsync();

        }

        public async Task<LikeModel> GetEntityByIdAsync(int id)
        {
            return await _db.Likes.FindAsync(id);
        }

        public void PostEntity(LikeModel like)
        {
            _db.Likes.Add(like);
        }

        public void DeleteEntity(int likeId)
        {
            LikeModel like = _db.Likes.Find(likeId);
            _db.Likes.Remove(like);
        }

        public void UpdateEntity(LikeModel like)
        {
            _db.Entry(like).State = EntityState.Modified;
        }


        private bool _disposed = false;

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