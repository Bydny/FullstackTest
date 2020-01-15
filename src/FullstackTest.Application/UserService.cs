using AutoMapper;
using FullstackTest.Application.Abstractions.Services;
using FullstackTest.Application.DTOs;
using FullstackTest.Persistence.Abstractions;
using System;
using System.Threading.Tasks;

namespace FullstackTest.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        protected readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDto> AuthenticateUserAsync(string email, string password)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentException("Invalid email");
            }

            if (user.Password != password)
            {
                throw new ArgumentException("Invalid password");
            }

            return mapper.Map<UserDto>(user);
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            return (await userRepository.GetByEmailAsync(email) != null);
        }
    }
}
