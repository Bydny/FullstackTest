using FullstackTest.Domain;
using System;
using System.Threading.Tasks;

namespace FullstackTest.Persistence.Abstractions
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
