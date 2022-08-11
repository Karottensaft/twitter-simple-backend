using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Data;

namespace SweaterV1.Infrastructure.Repositories;

public class CommentRepository : IRepository<CommentModel>
{
    private readonly SweaterDbContext _db;


    private bool _disposed;

    public CommentRepository(SweaterDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CommentModel>> GetEntityListAsync()
    {
        return await _db.Comments.ToListAsync();
    }

    public async Task<CommentModel> GetEntityByIdAsync(int commentId)
    {
        var comment = await _db.Comments.SingleOrDefaultAsync(x => x.CommentId == commentId);
        if (comment == null)
            throw new ArgumentNullException(nameof(comment), "Comment was null");
        return comment;
    }

    public void PostEntity(CommentModel comment)
    {
        _db.Comments.Add(comment);
    }

    public void DeleteEntity(int commentId)
    {
        var comment = _db.Comments.Find(commentId);
        if (comment == null)
            throw new ArgumentNullException(nameof(comment), "Comment was null");
        _db.Comments.Remove(comment);
    }

    public void UpdateEntity(CommentModel comment)
    {
        _db.Entry(comment).State = EntityState.Modified;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void DeleteAllEntitiesByPostId(int postId)
    {
        _db.Comments.RemoveRange(_db.Comments.Where(x => x.PostId == postId));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _db.Dispose();
        _disposed = true;
    }
}