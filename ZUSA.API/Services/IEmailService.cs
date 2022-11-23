using ZUSA.API.Models.Local;

namespace ZUSA.API.Services
{
    public interface IEmailService
    {
        Task<Result<bool>> SendEmailAsync(EmailRequest email);
    }
}