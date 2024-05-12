using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class AdminAccountRepository : IAdminAccountRepository
    {
        private readonly AppDbContext _context;

        public AdminAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAdminAccountAsync(AdminAccount adminAccount)
        {
            if (await UsernameExistsAsync(adminAccount.Username))
                return false;

            adminAccount.Password = HashPassword(adminAccount.Password);
            _context.AdminAccounts.Add(adminAccount);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var account = await _context.AdminAccounts.FirstOrDefaultAsync(x => x.Username == username);
            if (account == null || !VerifyPassword(password, account.Password))
                return null;

            // Generate token and refresh token
            var token = GenerateToken();
            var refreshToken = GenerateToken();
            account.Token = token;
            account.RefreshToken = refreshToken;
            account.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.AdminAccounts.AnyAsync(x => x.Username == username);
        }

        public async Task<IEnumerable<AdminAccount>> GetAllAdminAccountsAsync()
        {
            return await _context.AdminAccounts.ToListAsync();
        }

        public async Task<AdminAccount> GetAdminAccountByIdAsync(int id)
        {
            return await _context.AdminAccounts.FindAsync(id);
        }

        public async Task<AdminAccount> UpdateAdminAccountAsync(int id, AdminAccount updatedAdminAccount)
        {
            var adminAccount = await _context.AdminAccounts.FindAsync(id);
            if (adminAccount == null)
            {
                return null;
            }

            adminAccount.FirstName = updatedAdminAccount.FirstName;
            adminAccount.LastName = updatedAdminAccount.LastName;
            adminAccount.Username = updatedAdminAccount.Username;
            adminAccount.Password = HashPassword(updatedAdminAccount.Password);

            await _context.SaveChangesAsync();
            return adminAccount;
        }

        public async Task<bool> DeleteAdminAccountAsync(int id)
        {
            var adminAccount = await _context.AdminAccounts.FindAsync(id);
            if (adminAccount == null)
            {
                return false;
            }

            _context.AdminAccounts.Remove(adminAccount);
            await _context.SaveChangesAsync();
            return true;
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