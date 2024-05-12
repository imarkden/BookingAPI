using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Context;
using SaintJohnDentalClinicApi.Models.Entity;
using SaintJohnDentalClinicApi.Repositories.Interface;
using System.Globalization;

namespace SaintJohnDentalClinicApi.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBookAsync(Book book, string otpCode)
        {
            DateTime selectedDateTime = DateTime.ParseExact(book.SelectedTime, "HH:mm", CultureInfo.InvariantCulture);
            TimeSpan selectedTime = selectedDateTime.TimeOfDay;
            selectedTime = selectedTime.Add(TimeSpan.FromMinutes(30));
            book.EndTime = selectedDateTime.Date.Add(selectedTime).ToString("HH:mm");
            book.SelectedDate = book.SelectedDate.Date;
            book.Status = "Pending";

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            string message = $"Hi {book.FirstName}, \n\n" +
                $"Thank you for booking an appointment,\n your appointment will be verified soon.\n\n" +
                $"We will notify you again after confirming your appointment,\n\nKind Regards,\nSaint Anthony Dental Clinic";


            return book.Id;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IQueryable<Book>> GetAllBooksAsync()
        {
            return _context.Books.AsQueryable();
        }

        public async Task<IQueryable<Book>> GetBooksByStatusAsync(string status)
        {
            return _context.Books.Where(b => b.Status == status);
        }
    }
}
