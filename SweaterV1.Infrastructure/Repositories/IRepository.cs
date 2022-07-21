using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweaterV1.Domain.Models;

namespace SweaterV1.Infrastructure.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {

        Task<IEnumerable<T>> GetEntityListAsync(); // получение всех объектов
        Task<T> GetEntityByIdAsync(int id); // получение одного объекта по id
        void PostEntity(T item); // создание объекта
        void UpdateEntity(T item); // обновление объекта
        void DeleteEntity(int id); // удаление объекта по id
        Task SaveAsync(); // сохранение изменений
    }
}