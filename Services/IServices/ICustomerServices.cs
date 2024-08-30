using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerListVM>> GetAllCustomersAsync();
        Task<CustomerListVM> GetCustomerByIdAsync(int customerId);
        Task<CustomerListVM> GetCustomerByEmailAsync(string email);
        Task AddCustomerAsync(CustomerDto customer);
        Task UpdateCustomerAsync(CustomerUpdateDto customer);
        Task DeleteCustomerByEmailAsync(string email);
    }
}
