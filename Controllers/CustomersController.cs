using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;
        public CustomersController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult> AddCustomer(CustomerDto customer)
        {
            try
            {
                await _customerServices.AddCustomerAsync(customer);
                return Created();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<ActionResult> DeleteCustomerByEmail(CustomerEmailDto customer)
        {
            try
            {
                await _customerServices.DeleteCustomerByEmailAsync(customer.Email);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerListVM>>> GetAllCustomers()
        {
            try
            {
                return Ok(await _customerServices.GetAllCustomersAsync());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("GetCustomerByEmail")]
        public async Task<ActionResult<CustomerListVM>> GetCustomerByEmail(CustomerEmailDto customer)
        {
            try
            {
                return Ok(await _customerServices.GetCustomerByEmailAsync(customer.Email));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // kanske ta bort helt
        //[HttpGet("GetCustomerById")]

        [HttpPatch("UpdateCustomer")]
        public async Task<ActionResult> UpdateCustomer(CustomerUpdateDto customer)
        {
            try
            {
                await _customerServices.UpdateCustomerAsync(customer);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
