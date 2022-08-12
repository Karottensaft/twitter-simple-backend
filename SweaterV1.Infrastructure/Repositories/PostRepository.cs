using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories;

public class PostRepository : IRepository<PostModel>
{
    private readonly SweaterDbContext _db;


    private bool _disposed;

    public PostRepository(SweaterDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<PostModel>> GetEntityListAsync()
    {
        return await _db.Posts.ToListAsync();
    }

    public async Task<PostModel> GetEntityByIdAsync(int postId)
    {
        var post = await _db.Posts.SingleOrDefaultAsync(x => x.PostId == postId);
        if (post == null)
            throw new ArgumentNullException(nameof(post), "Post was null");
        return post;
    }


    public void PostEntity(PostModel post)
    {
        _db.Posts.Add(post);
    }


    public void UpdateEntity(PostModel post)
    {
        _db.Entry(post).State = EntityState.Modified;
    }

    public void DeleteEntity(int postId)
    {
        var post = _db.Posts.Find(postId);
        if (post == null)
            throw new ArgumentNullException(nameof(post), "Post was null");
        _db.Posts.Remove(post);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<PostModel>> GetEntityListAsyncByUserId(string username)
    {
        return await _db.Posts.Where(x => x.Username == username).ToListAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _db.Dispose();
        _disposed = true;
    }
}