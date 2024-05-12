namespace SaintJohnDentalClinicApi.Services.Interface
{
    public interface ISmsService
    {
        Task<string> SendSmsAsync(string toPhoneNumber, string message);
        Task<string> SendOTPCodeBySMS(string phoneNumber);
        string GenerateOTP();
    }
}
