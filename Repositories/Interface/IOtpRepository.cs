using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IOtpRepository
    {
        Task<string> GenerateOtpCodeAsync(string phoneNumber);
        Task<bool> VerifyOtpCodeAsync(string phoneNumber, string otpCode);
        Task<List<OtpCode>> GetAllCodes();
    }
}
