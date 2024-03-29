﻿using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Utility;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<Result<Account>> AddAsync(Account account);
        Task<Result<Account>> GetByIdAsync(int id);
        Task<Result<IEnumerable<Account>>> GetAllAsync();
        Task<Result<Account>> UpdateAsync(Account account);
        Task<Result<bool>> DeleteAsync(Account account);
        Task<Result<Account>> VerifyOtpAsync(VerifyOtpRequest request);
        Task<Result<Account>> LoginAsync(LoginRequest request);
        Task<Result<Account>> ChangePasswordAsync(ChangePasswordRequest changePassword);
        Task<Result<Pageable<Account>>> GetAllPagedAsync(Pagination pagination);
        Task<Result<string>> GetResetPasswordCodeAsync(string email);
        Task<Result<Account>> ResetPasswordAsync(ResetPasswordRequest resetPassword);
        Task<Result<string>> ResendOtpAsync(string email);
    }
}