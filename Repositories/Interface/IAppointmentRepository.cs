using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status);
        Task<Appointment> UpdateAppointmentAsync(int id, Appointment updatedAppointment);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
