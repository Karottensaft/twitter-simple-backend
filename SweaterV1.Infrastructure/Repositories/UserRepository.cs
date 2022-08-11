using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class UserRepository : IRepository<UserModel>
    {
        private readonly SweaterDbContext _db;

        public UserRepository(SweaterDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserModel>> GetEntityListAsync()
        {
            return await _db.Users.ToListAsync();
        }
        public async Task<UserModel> GetEntityByUsernameAsync(string username)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == username);
            return user;
        }

        public async Task<UserModel> GetEntityByIdAsync(int userId)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist.");
            }
            return user;
        }

        public void PostEntity(UserModel user)
        {
            _db.Users.Add(user);
        }

        public void DeleteEntity(int userId)
        {
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist.");
            }
            _db.Users.Remove(user);
        }

        public void UpdateEntity(UserModel user)
        {
            _db.Entry(user).State = EntityState.Modified;
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