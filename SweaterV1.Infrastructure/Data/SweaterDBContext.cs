using Microsoft.EntityFrameworkCore;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Infrastructure.Data
{
    public class SweaterDBContext : DbContext
    {    
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public SweaterDBContext(DbContextOptions<SweaterDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public SweaterDBContext()
        {
            //throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=UsersBD;Username=postgres;Password=Boss1234");
            //optionsBuilder.UseInMemoryDatabase("Users");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserModel>()
        //        .HasMany(c => c.Posts)
        //        .WithOne(o => o.User)
        //        .IsRequired();
        //}
    }
}