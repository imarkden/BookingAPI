using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            appointment.Status = "OnGoing";

            if (appointment.Duration.HasValue && !string.IsNullOrEmpty(appointment.AppointmentTime))
            {
                appointment.EndTime = CalculateEndTime(appointment.AppointmentTime, appointment.Duration.Value);
            }

            if (appointment.AdditionalCost.HasValue)
            {
                var service = await _context.Services.FindAsync(appointment.ServiceId);
                if (service != null)
                {
                    appointment.TotalAmount = service.Cost + appointment.AdditionalCost;
                }
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Book)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Book)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status)
        {
            return await _context.Appointments
                .Include(a => a.Book)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .Where(a => a.Status == status)
                .ToListAsync();
        }

        public async Task<Appointment> UpdateAppointmentAsync(int id, Appointment updatedAppointment)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return null;
            }

            appointment.AppointmentDate = updatedAppointment.AppointmentDate;
            appointment.AppointmentTime = updatedAppointment.AppointmentTime;
            appointment.Duration = updatedAppointment.Duration;
            appointment.EndTime = updatedAppointment.EndTime;
            appointment.AdditionalCost = updatedAppointment.AdditionalCost;
            appointment.TotalAmount = updatedAppointment.TotalAmount;
            appointment.Note = updatedAppointment.Note;
            appointment.Status = updatedAppointment.Status;

            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        private string CalculateEndTime(string appointmentTime, int duration)
        {
            if (DateTime.TryParse(appointmentTime, out DateTime startTime))
            {
                DateTime endTime = startTime.AddMinutes(duration);
                return endTime.ToString("HH:mm");
            }
            else
            {
                return null;
            }
        }
    }
}
