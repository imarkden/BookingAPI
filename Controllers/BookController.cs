using Microsoft.AspNetCore.Mvc;
using SaintJohnDentalClinicApi.Models.DTO;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Implementation;
using SaintJohnDentalClinicApi.Repositories.Interface;

namespace SaintJohnDentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOtpRepository _otpRepository;

        public BookController(IBookRepository bookRepository, IOtpRepository otpRepository)
        {
            _bookRepository = bookRepository;
            _otpRepository = otpRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBookAsync([FromBody] Book book, string otpCode)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool isValidOtp = await _otpRepository.VerifyOtpCodeAsync(book.Phone, otpCode);

                if (!isValidOtp)
                {
                    return BadRequest("Invalid or expired OTP code.");
                }

                var bookId = await _bookRepository.CreateBookAsync(book, otpCode);
                return CreatedAtAction(nameof(GetBookById), new { id = bookId }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("codes")]
        public async Task<IActionResult> GetAllCodes()
        {
            var guests = await _otpRepository.GetAllCodes();
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByStatus(string status)
        {
            var books = await _bookRepository.GetBooksByStatusAsync(status);
            return Ok(books);
        }

        [HttpPost("otp")]
        public async Task<IActionResult> GenerateOTP(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone number is required.");
            }

            try
            {
                string otpCode = await _otpRepository.GenerateOtpCodeAsync(phoneNumber);
                return Ok(otpCode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}