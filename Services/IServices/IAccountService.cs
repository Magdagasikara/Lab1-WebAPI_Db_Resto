using Lab1_WebAPI_Db_Resto.Models.DTOs.Account;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Booking;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountRegisterDto>> GetAllAccountsAsync();
        Task<AccountRegisterDto> GetAccountByEmailAsync(string email);
        Task AddAccountAsync(AccountRegisterDto account);
        Task<string> VerifyLoginAsync(AccountLoginDto accountToLogin);
    }
}
