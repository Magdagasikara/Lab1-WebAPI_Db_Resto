using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Account;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Customer;
using Lab1_WebAPI_Db_Resto.Services.IServices;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        public AccountService(IAccountRepository accountRepo, IMapper mapper, JwtService jwtService)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<AccountRegisterDto?> GetAccountByEmailAsync(string email)
        {
            try
            {
                var account = await _accountRepo.GetAccountByEmailAsync(email);
                return _mapper.Map<AccountRegisterDto>(account);
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<AccountRegisterDto>> GetAllAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAccountAsync(AccountRegisterDto accountToRegister)
        {
            try
            {
                var accountCheck = await GetAccountByEmailAsync(accountToRegister.Email);
                if (accountCheck != null)
                {
                    throw new Exception("Email already registered.");
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(accountToRegister.Password);

                var account = _mapper.Map<Account>(accountToRegister);
                account.PasswordHash = passwordHash;
                await _accountRepo.AddAccountAsync(account);
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> VerifyLoginAsync(AccountLoginDto accountToLogin)
        {
            try
            {
                var account = await _accountRepo.GetAccountByEmailAsync(accountToLogin.Email);
                if (account == null || !BCrypt.Net.BCrypt.Verify(accountToLogin.Password, account.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Wrong email or password.");
                }
                var token = _jwtService.GenerateJwtToken(account);
                return token;
            }
            catch
            {
                throw;
            }
        }
    }
}
