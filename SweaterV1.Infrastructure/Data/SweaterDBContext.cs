using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;


namespace SweaterV1.Infrastructure.Data
{
    public class SweaterDbContext : DbContext
    {
        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<PostModel> Posts => Set<PostModel>();
        public DbSet<CommentModel> Comments => Set<CommentModel>();
        public DbSet<LikeModel> Likes => Set<LikeModel>();

        public SweaterDbContext(DbContextOptions<SweaterDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}