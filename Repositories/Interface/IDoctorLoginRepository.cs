using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IDoctorLoginRepository
    {
        Task<bool> CreateDoctorLoginAsync(string username, string password, int doctorId);
        Task<string?> LoginAsync(string username, string password);
        Task<bool> UsernameExistsAsync(string username);
        Task<IEnumerable<DoctorAccount>> GetAllDoctorAccountsAsync();
        Task<DoctorAccount> GetDoctorAccountByIdAsync(int id);
        Task<DoctorAccount> UpdateDoctorAccountAsync(int id, DoctorAccount updatedDoctorAccount, string newPassword);
        Task<bool> DeleteDoctorAccountAsync(int id);
    }
}
