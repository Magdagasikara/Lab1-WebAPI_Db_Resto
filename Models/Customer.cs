using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }


        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
