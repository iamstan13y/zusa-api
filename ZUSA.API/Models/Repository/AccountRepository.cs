﻿using Microsoft.EntityFrameworkCore;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Services;
using ZUSA.API.Utility;

namespace ZUSA.API.Models.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IEmailService _emailService;

        public AccountRepository(AppDbContext context, IConfiguration configuration, IPasswordService passwordService, IJwtService jwtService, ICodeGeneratorService codeGeneratorService, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _codeGeneratorService = codeGeneratorService;
            _emailService = emailService;
        }

        public async Task<Result<Account>> AddAsync(Account account)
        {
            try
            {
                if (!IsUniqueUser(account.Email!))
                    return new Result<Account>(false, "An account with that email already exists!");

                account.Password = _passwordService.HashPassword(account.Password!);

                await _context.Accounts!.AddAsync(account);

                var code = await _codeGeneratorService.GenerateVerificationCode();

                await _context.OtpCodes!.AddAsync(new OtpCode
                {
                    Code = code,
                    Email = account.Email,
                    DateCreated = DateTime.Now
                });

                await _context.SaveChangesAsync();

                await _emailService.SendEmailAsync(new EmailRequest
                {
                    To = account.Email,
                    Subject = _configuration["EmailService:ConfirmAccountSubject"],
                    Body = string.Format(_configuration["EmailService:ConfirmAccountBody"], account.FirstName, code, account.Email)
                });

                return new Result<Account>(account, "Account created successfully!");
            }
            catch (Exception ex)
            {
                return new Result<Account>(false, ex.ToString());
            }
        }

        public Task<Result<bool>> DeleteAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<Account>>> GetAllAsync()
        {
            var users = await _context.Accounts!.ToListAsync();

            return new Result<IEnumerable<Account>>(users);
        }

        public async Task<Result<Account>> GetByIdAsync(int id)
        {
            var account = await _context.Accounts!.SingleOrDefaultAsync(x => x.Id == id);
            if (account == null)
                return new Result<Account>(false, "User not found");

            return new Result<Account>(account);
        }

        public Task<Result<Account>> UpdateAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Account>> VerifyOtpAsync(VerifyOtpRequest request)
        {
            var account = await _context.Accounts!.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (account == null) return new Result<Account>(false, "User account not found!");

            var code = await _context.OtpCodes!.Where(x => x.Email == request.Email && x.Code == request.Otp).FirstOrDefaultAsync();
            if (code == null) return new Result<Account>(false, "Invalid OTP code provided!");

            account.IsActive = true;

            _context.Accounts!.Update(account);
            await _context.SaveChangesAsync();

            return new Result<Account>(account, "Account registration complete!");
        }

        public async Task<Result<Account>> LoginAsync(LoginRequest login)
        {
            var account = await _context.Accounts!
                .Where(x => x.Email == login.Email)
                .FirstOrDefaultAsync();

            if (account != null && !account.IsActive) return new Result<Account>(false, "Please complete sign up process for this account!");

            if (account == null || _passwordService.VerifyHash(login.Password!, account!.Password!) == false)
                return new Result<Account>(false, "Username or password is incorrect!");

            account.Token = await _jwtService.GenerateToken(account);
            account.Password = "*************";

            return new Result<Account>(account);
        }

        private bool IsUniqueUser(string email)
        {
            var user = _context.Accounts!.SingleOrDefault(x => x.Email == email);

            if (user == null) return true;
            return false;
        }

        public async Task<Result<Account>> ChangePasswordAsync(ChangePasswordRequest changePassword)
        {
            var account = await GetByIdAsync(changePassword.UserId);
            if (!account.Success) return account;

            if (_passwordService.VerifyHash(changePassword.OldPassword!, account.Data!.Password!) == false)
                return new Result<Account>(false, "Old password mismatch");

            account.Data.Password = _passwordService.HashPassword(changePassword.NewPassword!);

            _context.Accounts!.Update(account.Data);
            await _context.SaveChangesAsync();

            return new Result<Account>(account.Data);
        }

        public async Task<Result<string>> ResendOtpAsync(string email)
        {
            var account = await _context
                .Accounts!
                .Where(x => x.Email!.Equals(email))
                .FirstOrDefaultAsync();

            if (account == null) return new Result<string>(false, "Please ensure you have recently created an account with us!");

            var otpCode = await _context.OtpCodes!.Where(x => x.Email == email && x.DateCreated.AddMinutes(10) >= DateTime.Now).FirstOrDefaultAsync();

            if (otpCode == null)
            {
                otpCode = new();
                otpCode!.Code = await _codeGeneratorService.GenerateVerificationCode();

                await _context.OtpCodes!.AddAsync(new OtpCode
                {
                    Code = otpCode!.Code,
                    Email = email,
                    DateCreated = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }

            var emailResult = await _emailService.SendEmailAsync(new EmailRequest
            {
                Subject = _configuration["EmailService:ConfirmAccountSubject"],
                Body = string.Format(_configuration["EmailService:ConfirmAccountBody"], account.FirstName, otpCode.Code, account.Email),
                To = email
            });

            if (!emailResult.Success) return new Result<string>("Failed to send email.");

            return new Result<string>("OTP code has been sent to your email.");
        }

        public async Task<Result<string>> GetResetPasswordCodeAsync(string email)
        {
            var account = await _context.Accounts!.SingleOrDefaultAsync(y => y.Email == email);
            if (account == null) return new Result<string>(false, "User account does not exist.");

            var verificationCode = await _codeGeneratorService.GenerateVerificationCode();

            await _context.OtpCodes!.AddAsync(new OtpCode
            {
                Code = verificationCode,
                Email = account.Email,
                DateCreated = DateTime.Now
            });

            await _context.SaveChangesAsync();

            var emailResult = await _emailService.SendEmailAsync(new EmailRequest
            {
                Body = string.Format(_configuration["EmailService:ResetPasswordBody"], account.FirstName, verificationCode),
                Subject = _configuration["EmailService:ResetPasswordSubject"],
                To = account.Email
            });

            if (!emailResult.Success) return new Result<string>(false, "Failure sending email.");

            return new Result<string>("Verification code has been sent to your email.");
        }

        public async Task<Result<Account>> ResetPasswordAsync(ResetPasswordRequest resetPassword)
        {
            var account = await _context.Accounts!.Where(x => x.Email == resetPassword.UserEmail).FirstOrDefaultAsync();
            var verifyCode = await _context.OtpCodes!
                .Where(x => x.Email == resetPassword.UserEmail &&
                x.Code == resetPassword.OtpCode)
                .FirstOrDefaultAsync();

            if (verifyCode == null) return new Result<Account>(false, "Invalid password reset code provided.");

            account!.Password = _passwordService.HashPassword(resetPassword.NewPassword!);

            _context.Update(account);
            await _context.SaveChangesAsync();

            return new Result<Account>(account, "Your password has been resetted successfully.");
        }
        
        public async Task<Result<Pageable<Account>>> GetAllPagedAsync(Pagination pagination)
        {
            var users = await _context.Accounts!.ToListAsync();

            return new Result<Pageable<Account>>(new Pageable<Account>(users, pagination.Page, pagination.Size));
        }
    }
}