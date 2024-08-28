using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerId);

    }
}
