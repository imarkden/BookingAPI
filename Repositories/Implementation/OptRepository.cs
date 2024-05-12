using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;
using SaintJohnDentalClinicApi.Services.Interface;
using SaintJohnDentalClinicApi.Services.Service;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class OtpRepository : IOtpRepository
    {
        private readonly ISmsService _smsService;
        private readonly Dictionary<string, string> _otpStorage;
        private readonly AppDbContext _appDbContext;
        public OtpRepository(ISmsService smsService, AppDbContext appDbContext)
        {
            _smsService = smsService;
            _otpStorage = new Dictionary<string, string>();
            _appDbContext = appDbContext;
        }

        public async Task<string> GenerateOtpCodeAsync(string phoneNumber)
        {
            string otpCode = GenerateOtp();

            var otpEntity = new OtpCode
            {
                PhoneNumber = phoneNumber,
                OTPCode = otpCode,
                CreatedAt = DateTime.UtcNow
            };

            await _smsService.SendSmsAsync(phoneNumber, $"Your OTP code is: {otpCode}");

            // Store the generated OTP code entity in the database
            _appDbContext.OtpCodes.Add(otpEntity);
            await _appDbContext.SaveChangesAsync();

            return otpCode;
        }

        public async Task<bool> VerifyOtpCodeAsync(string phoneNumber, string otpCode)
        {
            var otpEntity = await _appDbContext.OtpCodes.FirstOrDefaultAsync(c =>
                c.PhoneNumber == phoneNumber &&
                c.OTPCode == otpCode);

            if (otpEntity != null)
            {
                // Check if the OTP code is still valid (within the allowed time limit)
                TimeSpan timeElapsed = DateTime.UtcNow - otpEntity.CreatedAt;
                if (timeElapsed.TotalMinutes <= 5)
                {
                    _appDbContext.OtpCodes.Remove(otpEntity); // Remove the OTP code from the database
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }

        public async Task<List<OtpCode>> GetAllCodes()
        {
            return await _appDbContext.OtpCodes.ToListAsync();
        }
    }
}
