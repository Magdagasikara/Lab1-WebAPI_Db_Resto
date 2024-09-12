using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Customer;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);
        Task<CustomerDto> GetCustomerByEmailAsync(string email);
        Task AddCustomerAsync(CustomerDto customer);
        Task UpdateCustomerAsync(CustomerUpdateDto customer);
        Task DeleteCustomerByEmailAsync(string email);
    }
}
