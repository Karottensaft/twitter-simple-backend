using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private SweaterDBContext _db = new SweaterDBContext();
        private UserRepository _userRepository;
        private PostRepository _postRepository;

        public UserRepository UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_db);
                }
                return _userRepository;
            }
        }

        public PostRepository PostRepository
        {
            get
            {

                if (this._postRepository == null)
                {
                    this._postRepository = new PostRepository(_db);
                }

                return _postRepository;
            }
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