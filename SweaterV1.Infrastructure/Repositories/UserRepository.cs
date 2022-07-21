using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories
{
    public class UserRepository : IRepository<UserModel>, IDisposable
    {
        private SweaterDBContext _db;

        public UserRepository(SweaterDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<UserModel>> GetEntityListAsync()
        {
            return await _db.Users.ToListAsync();
        }
        //async
        public async Task<UserModel> GetEntityByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }
         // not async
        public void PostEntity(UserModel user)
        {
            _db.Users.Add(user);
        }
        //not async
        public void DeleteEntity(int userId)
        {
            var user = _db.Users.Find(userId);
            _db.Users.Remove(user);
        }
        //not async
        public void UpdateEntity(UserModel user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }
        //async
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