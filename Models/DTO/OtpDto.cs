namespace SaintJohnDentalClinicApi.Models.DTO
{
    public class OtpDto
    {
        public string PhoneNumber { get; set; }
        public string OTPCode { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
