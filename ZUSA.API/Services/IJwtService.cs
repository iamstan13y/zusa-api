using ZUSA.API.Models.Data;

namespace ZUSA.API.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(Account account);
    }
}