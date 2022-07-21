using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Infrastructure
{
    public class PostgreSQLUsersRepository : IRepository<UserModel>
    {
        private SweaterDBContext _db;

        public PostgreSQLUsersRepository()
        {
            this._db = new SweaterDBContext(options: null);
        }

        public IEnumerable<UserModel> GetUserList()
        {
            return _db.Users;
        }

        public UserModel GetUser(int id)
        {
            return _db.Users.Find(id);
        }

        public void Create(UserModel user)
        {
            _db.Users.Add(user);
        }

        public void Update(UserModel user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserModel user = _db.Users.Find(id);
            if (user != null)
                _db.Users.Remove(user);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
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
