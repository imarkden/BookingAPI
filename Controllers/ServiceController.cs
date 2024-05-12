using Microsoft.AspNetCore.Mvc;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Service>> CreateService(Service service)
        {
            var createdService = await _serviceRepository.CreateServiceAsync(service);
            return CreatedAtAction(nameof(GetServiceById), new { id = createdService.Id }, createdService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetServiceById(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            return Ok(await _serviceRepository.GetAllServicesAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, Service service)
        {
            var updatedService = await _serviceRepository.UpdateServiceAsync(id, service);
            if (updatedService == null)
                return BadRequest("Service not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _serviceRepository.DeleteServiceAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
