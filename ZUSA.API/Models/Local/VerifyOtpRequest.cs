namespace ZUSA.API.Models.Local
{
    public class VerifyOtpRequest
    {
        public string? Email { get; set; }
        public string? Otp { get; set; }
    }
}