using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<bool> InactivateDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return false;

            doctor.Status = "Inactive";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Doctor?> UpdateDoctorAsync(int id, Doctor doctor)
        {
            if (id != doctor.Id)
                return null;

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                    return null;
                else
                    throw;
            }

            return doctor;
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
