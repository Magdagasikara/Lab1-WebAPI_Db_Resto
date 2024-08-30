using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(string email, Customer updatedCustomer);
        Task DeleteCustomerByEmailAsync(string email);

    }
}
