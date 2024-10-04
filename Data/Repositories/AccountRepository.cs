using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RestoContext _context;
        public AccountRepository(RestoContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Account account)
        {
            try
            {
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding a new account", ex);
            }
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            try
            {
                var account = await _context.Accounts
                    .SingleOrDefaultAsync(c => c.Email == email);
                // I need null if no account
                return account;

                throw new KeyNotFoundException($"Account with {email} not found");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting an account", ex);
            }
        }
    }
}
