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
            this._db = db;
        }

        public async Task<UserModel> LoginAsync(string login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Login == login);// && x.Password.ToLower() == password);
            return user;
        }

        public async Task<IEnumerable<UserModel>> GetEntityListAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<UserModel> GetEntityByIdAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            return user!;
        }

        public void PostEntity(UserModel user)
        {
            _db.Users.Add(user);
        }

        public void DeleteEntity(int userId)
        {
            var user = _db.Users.Find(userId);
            _db.Users.Remove(user!);
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