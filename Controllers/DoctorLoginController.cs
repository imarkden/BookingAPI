using Microsoft.AspNetCore.Mvc;
using SaintJohnDentalClinicApi.Models.DTO;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Implementation;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorLoginController : ControllerBase
    {
        private readonly IDoctorLoginRepository _doctorLoginRepository;

        public DoctorLoginController(IDoctorLoginRepository doctorLoginRepository)
        {
            _doctorLoginRepository = doctorLoginRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, int doctorId)
        {
            try
            {
                var result = await _doctorLoginRepository.CreateDoctorLoginAsync(username, password, doctorId);
                if (result)
                    return Ok("Doctor account created successfully");
                else
                    return BadRequest("Username already exists");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var token = await _doctorLoginRepository.LoginAsync(username, password);
                if (token != null)
                    return Ok(new { Token = token });
                else
                    return Unauthorized("Invalid username or password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorAccount>>> GetAllDoctorAccounts()
        {
            var doctorAccounts = await _doctorLoginRepository.GetAllDoctorAccountsAsync();
            return Ok(doctorAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorAccount>> GetDoctorAccountById(int id)
        {
            var doctorAccount = await _doctorLoginRepository.GetDoctorAccountByIdAsync(id);
            if (doctorAccount == null)
                return NotFound();
            return Ok(doctorAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctorAccount(int id, DoctorAccount updatedDoctorAccount, string newPassword)
        {
            var result = await _doctorLoginRepository.UpdateDoctorAccountAsync(id, updatedDoctorAccount, newPassword);
            if (result == null)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorAccount(int id)
        {
            var result = await _doctorLoginRepository.DeleteDoctorAccountAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}