using System.ComponentModel.DataAnnotations;
namespace SaintJohnDentalClinicApi.Models.Entity
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public DateTime SelectedDate { get; set; }
        public string? SelectedTime { get; set; }
        public string? EndTime { get; set; }
    }
}
