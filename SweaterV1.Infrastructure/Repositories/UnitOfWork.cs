using Microsoft.EntityFrameworkCore;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private SweaterDBContext _db;
        //private UserRepository _userRepository;
        //private PostRepository _postRepository;

        public UnitOfWork(DbContextOptions<SweaterDBContext> options)
        {
            _db = new SweaterDBContext(options);
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

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}