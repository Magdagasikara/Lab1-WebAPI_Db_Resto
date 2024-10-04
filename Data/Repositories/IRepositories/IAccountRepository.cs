using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Account;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface IAccountRepository
    {
        Task AddAccountAsync(Account account);
        Task<Account?> GetAccountByEmailAsync(string email);
    }
}
