using Microsoft.EntityFrameworkCore;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly SweaterDbContext _db;

        public UnitOfWork(DbContextOptions<SweaterDbContext> options)
        {
            _db = new SweaterDbContext(options);
            UserRepository = new UserRepository(_db);
            PostRepository = new PostRepository(_db);
        }
        public UserRepository UserRepository
        {
            get;
            private set;

        }

        public PostRepository PostRepository
        {
            get;
            private set;

        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
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