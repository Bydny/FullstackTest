using FullstackTest.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullstackTest.Persistence.Abstractions
{
    public interface IRepository<T, K> 
        where T : class, IIdentity<K>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(K id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(K id);
    }
}
