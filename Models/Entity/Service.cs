using System.ComponentModel.DataAnnotations;

namespace SaintJohnDentalClinicApi.Models.Entity
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Cost { get; set; }
        public string? Description { get; set; }
    }
}
