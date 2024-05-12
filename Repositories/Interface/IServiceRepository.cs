using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IServiceRepository
    {
        Task<Service> CreateServiceAsync(Service service);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service?> GetServiceByIdAsync(int id);
        Task<Service?> UpdateServiceAsync(int id, Service service);
        Task<bool> DeleteServiceAsync(int id);
    }
}
