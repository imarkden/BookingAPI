using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Repositories.Interface
{
    public interface IBookRepository
    {
        Task<int> CreateBookAsync(Book book, string otpCode);
        Task<Book?> GetBookByIdAsync(int id);
        Task<IQueryable<Book>> GetAllBooksAsync();
        Task<IQueryable<Book>> GetBooksByStatusAsync(string status);
    }
}
