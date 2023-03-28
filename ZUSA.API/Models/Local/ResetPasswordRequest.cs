namespace ZUSA.API.Models.Local
{
    public class ResetPasswordRequest
    {
        public string? UserEmail { get; set; }
        public string? OtpCode { get; set; }
        public string? NewPassword { get; set; }
    }
}