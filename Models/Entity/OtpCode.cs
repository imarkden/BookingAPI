using System.ComponentModel.DataAnnotations;

namespace SaintJohnDentalClinicApi.Models.Entity
{
    public class OtpCode
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string OTPCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
