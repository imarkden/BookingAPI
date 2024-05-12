using Microsoft.AspNetCore.Mvc;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminAccountController : ControllerBase
    {
        private readonly IAdminAccountRepository _adminAccountRepository;

        public AdminAccountController(IAdminAccountRepository adminAccountRepository)
        {
            _adminAccountRepository = adminAccountRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AdminAccount adminAccount)
        {
            try
            {
                if (await _adminAccountRepository.UsernameExistsAsync(adminAccount.Username))
                    return BadRequest("Username already exists");

                bool result = await _adminAccountRepository.CreateAdminAccountAsync(adminAccount);

                if (result)
                    return Ok("Admin account created successfully");
                else
                    return StatusCode(500, "Failed to create admin account");
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
                string? token = await _adminAccountRepository.LoginAsync(username, password);
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
        public async Task<IActionResult> GetAllAdminAccounts()
        {
            try
            {
                var adminAccounts = await _adminAccountRepository.GetAllAdminAccountsAsync();
                return Ok(adminAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminAccountById(int id)
        {
            try
            {
                var adminAccount = await _adminAccountRepository.GetAdminAccountByIdAsync(id);
                if (adminAccount == null)
                    return NotFound();

                return Ok(adminAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdminAccount(int id, AdminAccount updatedAdminAccount)
        {
            try
            {
                var adminAccount = await _adminAccountRepository.UpdateAdminAccountAsync(id, updatedAdminAccount);
                if (adminAccount == null)
                    return NotFound();

                return Ok(adminAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminAccount(int id)
        {
            try
            {
                var result = await _adminAccountRepository.DeleteAdminAccountAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}