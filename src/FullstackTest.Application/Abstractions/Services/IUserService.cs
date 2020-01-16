using System;
using FullstackTest.Application.DTOs;
using System.Threading.Tasks;

namespace FullstackTest.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserDto> AuthenticateUserAsync(string email, string password);
        Task<bool> CheckIfEmailExistsAsync(string email);
    }
}
