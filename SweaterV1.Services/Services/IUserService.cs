using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweaterV1.Domain.Models;

namespace SweaterV1.Services.Services
{
    public interface IService<T>
        where T : class
    {
        Task<IEnumerable<T>> GerListOfEntities();
        Task<T> GetEntity(int id);
        Task CreateEntity(T item);
        Task UpdateEntity(T item);
        Task DeleteEntity(int id);
    }
}
