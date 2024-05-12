namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface ISuperAdminRepository
    {
        Task<bool> CreateAdminAsync(string username, string password);
        Task<string?> LoginAsync(string username, string password);
    }
}
