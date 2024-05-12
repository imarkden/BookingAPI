using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly AppDbContext _context;

        public SuperAdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAdminAsync(string username, string password)
        {
            if (await _context.SuperAdmins.AnyAsync(x => x.Username == username))
                return false;

            var hashedPassword = HashPassword(password);
            var admin = new SuperAdmin
            {
                Username = username,
                Password = hashedPassword
            };

            _context.SuperAdmins.Add(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var admin = await _context.SuperAdmins.FirstOrDefaultAsync(x => x.Username == username);
            if (admin == null || !VerifyPassword(password, admin.Password))
                return null; // Invalid username or password

            // Generate token and refresh token
            var token = GenerateToken();
            var refreshToken = GenerateToken();
            admin.Token = token;
            admin.RefreshToken = refreshToken;
            admin.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();
            return token;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }

        private string GenerateToken()
        {
            var token = Guid.NewGuid().ToString();
            return token;
        }
    }
}
