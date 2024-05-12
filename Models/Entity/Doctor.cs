﻿using System.ComponentModel.DataAnnotations;

namespace SaintJohnDentalClinicApi.Models.Entity
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName{ get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
    }
}
