using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories;

public class LikeRepository : IRepository<LikeModel>
{
    private readonly SweaterDbContext _db;


    private bool _disposed;

    public LikeRepository(SweaterDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<LikeModel>> GetEntityListAsync()
    {
        return await _db.Likes.ToListAsync();
    }

    public async Task<LikeModel> GetEntityByIdAsync(int likeId)
    {
        var like = await _db.Likes.SingleOrDefaultAsync(x => x.LikeId == likeId);
        if (like == null)
            throw new ArgumentNullException(nameof(like), "Like was null");
        return like;
    }


    public void PostEntity(LikeModel like)
    {
        _db.Likes.Add(like);
    }


    public void UpdateEntity(LikeModel like)
    {
        _db.Entry(like).State = EntityState.Modified;
    }

    public void DeleteEntity(int likeId)
    {
        var like = _db.Likes.Find(likeId);
        if (like == null)
            throw new ArgumentNullException(nameof(like), "Like was null");
        _db.Likes.Remove(like);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<LikeModel>> GetEntityListAsyncByPostId(int postId)
    {
        return await _db.Likes.Where(x => x.PostId == postId).ToListAsync();
    }

    public void DeleteAllEntitiesByPostId(int postId)
    {
        _db.Likes.RemoveRange(_db.Likes.Where(x => x.PostId == postId));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _db.Dispose();
        _disposed = true;
    }
}