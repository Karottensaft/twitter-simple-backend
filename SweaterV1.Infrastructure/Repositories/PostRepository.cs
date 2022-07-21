using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class PostRepository : IRepository<PostModel>, IDisposable
    {
        private SweaterDBContext _db;

        public PostRepository(SweaterDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<PostModel>> GetEntityListAsync()
        {
            return await _db.Posts.ToListAsync();
        }

        public async Task<PostModel> GetEntityByIdAsync(int id)
        {
            return await _db.Posts.FindAsync(id);
        }

        public void PostEntity(PostModel post)
        {
            _db.Posts.Add(post);
        }

        public void DeleteEntity(int postId)
        {
            PostModel post = _db.Posts.Find(postId);
            _db.Posts.Remove(post);
        }

        public void UpdateEntity(PostModel post)
        {
            _db.Entry(post).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
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
