using System.Threading.Tasks;

namespace FullstackTest.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateTokenAsync(string email, string password);
    }
}
