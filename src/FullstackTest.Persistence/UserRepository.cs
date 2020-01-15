using FullstackTest.Domain;
using FullstackTest.Persistence.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FullstackTest.Persistence
{
    public class UserRepository : EFRepository<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await DbSet.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
