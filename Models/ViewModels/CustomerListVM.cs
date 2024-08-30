using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.ViewModels
{
    public class CustomerListVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
