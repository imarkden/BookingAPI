using Microsoft.AspNetCore.Mvc;
using System;
using SaintJohnDentalClinicApi.Models.DTO;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/superadmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminController(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                var result = await _superAdminRepository.CreateAdminAsync(model.Username, model.Password);
                if (result)
                    return Ok("Admin created successfully");
                else
                    return BadRequest("Username already exists");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var token = await _superAdminRepository.LoginAsync(model.Username, model.Password);
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
    }
}
