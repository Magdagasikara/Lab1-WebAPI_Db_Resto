using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestoContext _context;
        public CustomerRepository(RestoContext context)
        {
            _context = context;
        }
        public async Task AddCustomer(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding a new customer", ex);
            }
        }

        public async Task DeleteCustomer(int customerId)
        {
            try
            {
                var customer = await GetCustomerById(customerId);
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when deleting a customer", ex);
            }

        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of customers",ex);
            }

        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer != null)
                {
                    return customer;
                }
                throw new KeyNotFoundException($"CustomerId {customerId} does not return a customer");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a customer", ex);
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer to be updated is null");
            }
            try
            {
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Customer to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a customer",ex);
            }
        }
    }
}
