using System;
using FullstackTest.Application.DTOs;
using System.Threading.Tasks;

namespace FullstackTest.Application.Abstractions.Services
{
    public interface IUserService
    {
        public Task<UserDto> AuthenticateUserAsync(string email, string password);
        public Task<bool> CheckIfEmailExistsAsync(string email);
    }
}
