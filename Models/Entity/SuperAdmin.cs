﻿using System.ComponentModel.DataAnnotations;

namespace SaintJohnDentalClinicApi.Models.Entity
{
    public class SuperAdmin
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password{ get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}