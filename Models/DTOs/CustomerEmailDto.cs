using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs
{
    public class CustomerEmailDto
    {
        [EmailAddress(ErrorMessage = "Please fill in correct Email-address")]
        [StringLength(100, MinimumLength = 6)]
        public string Email { get; set; }

    }
}
