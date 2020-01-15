using FullstackTest.Domain;
using FullstackTest.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullstackTest.Persistence
{
    public class EFRepository<T, K> : IRepository<T, K> 
        where T : class, IIdentity<K>
    {
        protected readonly ApplicationDbContext context;
        protected readonly DbSet<T> DbSet;

        public EFRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(K id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> CreateAsync(T item)
        {
            await DbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return await DbSet.FindAsync(item.Id);
        }

        public async Task<T> UpdateAsync(T item)
        {
            DbSet.Update(item);
            await context.SaveChangesAsync();
            return await DbSet.FindAsync(item.Id);
        }

        public async Task DeleteAsync(K id)
        {
            var item = await DbSet.FindAsync(id);
            DbSet.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
