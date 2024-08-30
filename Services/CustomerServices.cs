using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public CustomerServices(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task AddCustomerAsync(CustomerDto customer)
        {
            try
            {
                var newCustomer = _mapper.Map<Customer>(customer);
                await _customerRepo.AddCustomerAsync(newCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer in service", ex);
            }
        }

        public async Task DeleteCustomerByEmailAsync(string email)
        {
            try
            {
                await _customerRepo.DeleteCustomerByEmailAsync(email);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting customer in service", ex);
            }
        }

        public async Task<IEnumerable<CustomerListVM>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepo.GetAllCustomersAsync();

                return _mapper.Map<List<CustomerListVM>>(customers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customers in service", ex);
            }
        }

        public async Task<CustomerListVM> GetCustomerByEmailAsync(string email)
        {
            try
            {
                var customer = await _customerRepo.GetCustomerByEmailAsync(email);
                return _mapper.Map<CustomerListVM>(customer);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer in service", ex);
            }
        }

        public async Task<CustomerListVM> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await _customerRepo.GetCustomerByIdAsync(customerId);
                return _mapper.Map<CustomerListVM>(customer);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer in service", ex);
            }

        }

        public async Task UpdateCustomerAsync(CustomerUpdateDto customer)
        {
            try
            {
                var updatedCustomer = _mapper.Map<Customer>(customer);
                if (customer.UpdatedEmail is not null)
                {
                    updatedCustomer.Email = customer.UpdatedEmail;
                }
                await _customerRepo.UpdateCustomerAsync(customer.Email, updatedCustomer);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer in service", ex);
            }
        }
    }
}
