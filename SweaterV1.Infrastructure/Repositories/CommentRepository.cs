using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class CommentRepository : IRepository<CommentModel>, IDisposable
    {
        private readonly SweaterDbContext _db;

        public CommentRepository(SweaterDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<CommentModel>> GetEntityListAsync()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task<CommentModel> GetEntityByIdAsync(int id)
        {
            return await _db.Comments.FindAsync(id);
        }

        public void PostEntity(CommentModel comment)
        {
            _db.Comments.Add(comment);
        }

        public void DeleteEntity(int commentId)
        {
            CommentModel comment = _db.Comments.Find(commentId);
            _db.Comments.Remove(comment);
        }

        public void UpdateEntity(CommentModel comment)
        {
            _db.Entry(comment).State = EntityState.Modified;
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