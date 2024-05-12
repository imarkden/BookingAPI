using Microsoft.AspNetCore.Mvc;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.Id }, createdAppointment);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByStatus(string status)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByStatusAsync(status);
            return Ok(appointments);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment updatedAppointment)
        {
            var result = await _appointmentRepository.UpdateAppointmentAsync(id, updatedAppointment);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentRepository.DeleteAppointmentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}