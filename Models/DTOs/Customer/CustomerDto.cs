using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Customer
{
    public class CustomerDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Please fill in correct Email-address")]
        [StringLength(100, MinimumLength = 6)]
        public string Email { get; set; }
        [StringLength(20)]
        public string? PhoneNumber { get; set; }


    }
}
