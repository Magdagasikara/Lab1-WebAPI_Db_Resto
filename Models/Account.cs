using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
