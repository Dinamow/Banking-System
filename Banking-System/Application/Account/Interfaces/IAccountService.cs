using Banking_System.Core.Models;

namespace Banking_System.Application.Account.Interfaces
{
    public interface IAccountService
    {
        Task<bool> AccountExistsAsync(int id);
        Task<BankingAccount> GetAccountAsync(int id);
        Task<ICollection<BankingAccount>> GetMyAccountsAsync(string UserId);
        Task<BankingAccount> GetMyAccountAsync(string UserId, int AccountId);
        Task<BankingAccount> CreateAccountAsync(string UserId, string type);
    }
}
