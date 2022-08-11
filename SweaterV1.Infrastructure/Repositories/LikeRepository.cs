﻿using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class LikeRepository : IRepository<LikeModel>
    {
        private readonly SweaterDbContext _db;

        public LikeRepository(SweaterDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<LikeModel>> GetEntityListAsync()
        {
            return await _db.Likes.ToListAsync();
        }

        public async Task<IEnumerable<LikeModel>> GetEntityListAsyncByPostId(int postId)
        {
            return await _db.Likes.Where(x => x.PostId == postId).ToListAsync();

        }

        public async Task<LikeModel> GetEntityByIdAsync(int likeId)
        {
            var like = await _db.Likes.SingleOrDefaultAsync(x => x.LikeId == likeId);
            if (like == null)
            {
                throw new Exception("Like doesn't exist.");
            }
            return like;
        }

        public void PostEntity(LikeModel like)
        {
            _db.Likes.Add(like);
        }

        public void DeleteEntity(int likeId)
        {
            var like = _db.Likes.Find(likeId);
            if (like == null)
            {
                throw new Exception("Like doesn't exist.");
            }
            _db.Likes.Remove(like);
        }

        public void UpdateEntity(LikeModel like)
        {
            _db.Entry(like).State = EntityState.Modified;
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