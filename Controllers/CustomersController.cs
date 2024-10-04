using Lab1_WebAPI_Db_Resto.Models.DTOs.Customer;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerServices;
        public CustomersController(ICustomerService customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpPost("customer/add")]
        public async Task<ActionResult> AddCustomer(CustomerDto customer)
        {
            try
            {
                await _customerServices.AddCustomerAsync(customer);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("customer/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCustomerByEmail(CustomerEmailDto customer)
        {
            try
            {
                await _customerServices.DeleteCustomerByEmailAsync(customer.Email);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // default route
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            try
            {
                return Ok(await _customerServices.GetAllCustomersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("customer")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByEmail(CustomerEmailDto customer)
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
                return BadRequest(ex.Message);
            }
        }

        // kanske ta bort helt
        //[HttpGet("GetCustomerById")]

        [HttpPatch("customer/update")]
        public async Task<ActionResult> UpdateCustomer(CustomerUpdateDto customer)
        {
            try
            {
                await _customerServices.UpdateCustomerAsync(customer);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
