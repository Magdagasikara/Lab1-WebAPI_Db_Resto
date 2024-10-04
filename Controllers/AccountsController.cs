using Lab1_WebAPI_Db_Resto.Models.DTOs.Account;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(AccountRegisterDto accountToRegister)
        {
            try
            {
                await _accountService.AddAccountAsync(accountToRegister);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(AccountLoginDto accountToLogin)
        {
            try
            {
                var token = await _accountService.VerifyLoginAsync(accountToLogin);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
