using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaintJohnDentalClinicApi.Models.Entity
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public int? Duration { get; set; }
        public string? EndTime { get; set; }
        public double? AdditionalCost { get; set; }
        public double? TotalAmount { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }

        public int BookId { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }


        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
    }
}
