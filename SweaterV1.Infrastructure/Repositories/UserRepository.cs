using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories;

public class UserRepository : IRepository<UserModel>
{
    private readonly SweaterDbContext _db;

    private bool _disposed;

    public UserRepository(SweaterDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserModel>> GetEntityListAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<UserModel> GetEntityByIdAsync(int userId)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.UserId == userId);
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User was null");
        return user;
    }

    public void PostEntity(UserModel user)
    {
        _db.Users.Add(user);
    }

    public void UpdateEntity(UserModel user)
    {
        _db.Entry(user).State = EntityState.Modified;
    }

    public void DeleteEntity(int userId)
    {
        var user = _db.Users.Find(userId);
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User was null");
        _db.Users.Remove(user);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<UserModel> GetEntityByNameAsync(string username)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == username);
        return user!;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _db.Dispose();
        _disposed = true;
    }
}