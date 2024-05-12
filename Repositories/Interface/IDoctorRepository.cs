using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<bool> InactivateDoctorAsync(int id);
        Task<Doctor?> UpdateDoctorAsync(int id, Doctor doctor);
    }
}
