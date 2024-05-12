using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IAdminAccountRepository
    {
        Task<bool> CreateAdminAccountAsync(AdminAccount adminAccount);
        Task<string?> LoginAsync(string username, string password);
        Task<bool> UsernameExistsAsync(string username);
        Task<IEnumerable<AdminAccount>> GetAllAdminAccountsAsync();
        Task<AdminAccount> GetAdminAccountByIdAsync(int id);
        Task<AdminAccount> UpdateAdminAccountAsync(int id, AdminAccount updatedAdminAccount);
        Task<bool> DeleteAdminAccountAsync(int id);
    }
}
