namespace SweaterV1.Infrastructure.Repositories;

public interface IRepository<T> : IDisposable
    where T : class
{
    Task<IEnumerable<T>> GetEntityListAsync();
    Task<T> GetEntityByIdAsync(int id);
    void PostEntity(T item);
    void UpdateEntity(T item);
    void DeleteEntity(int id);
}