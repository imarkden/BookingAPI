using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class DoctorLoginRepository : IDoctorLoginRepository
    {
        private readonly AppDbContext _context;

        public DoctorLoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDoctorLoginAsync(string username, string password, int doctorId)
        {
            if (await UsernameExistsAsync(username))
                return false;

            var hashedPassword = HashPassword(password);

            var doctorAccount = new DoctorAccount
            {
                Username = username,
                Password = hashedPassword,
                DoctorId = doctorId
            };

            _context.DoctorAccounts.Add(doctorAccount);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var account = await _context.DoctorAccounts.FirstOrDefaultAsync(x => x.Username == username);
            if (account == null || !VerifyPassword(password, account.Password))
                return null;

            var token = GenerateToken();
            account.Token = token;
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<IEnumerable<DoctorAccount>> GetAllDoctorAccountsAsync()
        {
            return await _context.DoctorAccounts
                .Include(da => da.Doctor)
                .ToListAsync();
        }

        public async Task<DoctorAccount> GetDoctorAccountByIdAsync(int id)
        {
            return await _context.DoctorAccounts
                .Include(da => da.Doctor)
                .FirstOrDefaultAsync(da => da.Id == id);
        }

        public async Task<DoctorAccount> UpdateDoctorAccountAsync(int id, DoctorAccount updatedDoctorAccount, string newPassword)
        {
            var doctorAccount = await _context.DoctorAccounts.FindAsync(id);
            if (doctorAccount == null)
            {
                return null;
            }

            doctorAccount.Username = updatedDoctorAccount.Username;
            doctorAccount.DoctorId = updatedDoctorAccount.DoctorId;

            // Check if a new password is provided and hash it
            if (!string.IsNullOrEmpty(newPassword))
            {
                doctorAccount.Password = HashPassword(newPassword);
            }

            await _context.SaveChangesAsync();
            return doctorAccount;
        }

        public async Task<bool> DeleteDoctorAccountAsync(int id)
        {
            var doctorAccount = await _context.DoctorAccounts.FindAsync(id);
            if (doctorAccount == null)
            {
                return false;
            }

            _context.DoctorAccounts.Remove(doctorAccount);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.DoctorAccounts.AnyAsync(x => x.Username == username);
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
