using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestoContext _context;
        public CustomerRepository(RestoContext context)
        {
            _context = context;
        }
        public async Task AddCustomerAsync(Customer customer)
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

        public async Task DeleteCustomerByEmailAsync(string email)
        {
            try
            {
                var customer = await GetCustomerByEmailAsync(email);
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

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of customers", ex);
            }

        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            try
            {
                var customer = await _context.Customers
                    .SingleOrDefaultAsync(c => c.Email == email);
                if (customer != null)
                {
                    return customer;
                }
                throw new KeyNotFoundException($"No customer with {email}");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a customer", ex);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
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

        public async Task UpdateCustomerAsync(string email, Customer updatedCustomer)
        {
            if (updatedCustomer == null || email.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(updatedCustomer), "No customer to be updated");
            }
            try
            {
                var customer = await GetCustomerByEmailAsync(email);
                customer.Name = updatedCustomer.Name;
                customer.Email = updatedCustomer.Email;
                customer.PhoneNumber = updatedCustomer.PhoneNumber;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Customer to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a customer", ex);
            }
        }
    }
}
