using Microsoft.AspNetCore.Mvc;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            var createdDoctor = await _doctorRepository.CreateDoctorAsync(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { id = createdDoctor.Id }, createdDoctor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            return Ok(await _doctorRepository.GetAllDoctorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            var updatedDoctor = await _doctorRepository.UpdateDoctorAsync(id, doctor);
            if (updatedDoctor == null)
                return BadRequest("Doctor not found");

            return NoContent();
        }

        [HttpPut("{id}/inactivate")]
        public async Task<IActionResult> InactivateDoctor(int id)
        {
            var result = await _doctorRepository.InactivateDoctorAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
