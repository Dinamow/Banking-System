using Banking_System.Application.Account.Interfaces;
using Banking_System.Core.Models;
using Banking_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banking_System.Application.Account.UseCases
{
    public class AcocuntService : IAccountService
    {
        public readonly BankingDataContext _context;
        public AcocuntService(BankingDataContext context)
        {
            _context = context;
        }
        public Task<bool> AccountExistsAsync(int id)
        {
            return _context.Accounts.AnyAsync(A => A.Id == id);
        }

        public Task<BankingAccount> CreateAccountAsync(string UserId, string type)
        {
            if (type != "SavingAccount" && type != "CheckingAccount")
            {
                throw new ArgumentException("Invalid account type. Must be 'SavingAccount' or 'CheckingAccount'.");
            }

            if (HasTwoOrMoreAccountsOfTypeAsync(UserId, type).Result)
            {
                throw new InvalidOperationException($"User already has two or more {type} accounts.");
            }
            var account = new BankingAccount
            {
                AccountNumber = Guid.NewGuid().ToString(),
                AccountType = type,
                Balance = 0,
                OverDraftLimit = 0,
                IntrestRate = 0,
                AccumulatedInterest = 0,
                LastInterestCalculationDate = DateTime.Now,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                UserId = UserId,
                User = _context.Users.Find(UserId) ?? throw new InvalidOperationException("User not found.")
            };
            var result = _context.Accounts.Add(account);
            _context.SaveChanges();
            return Task.FromResult(result.Entity);
        }

        public Task<BankingAccount> GetAccountAsync(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found.");
            }
            return Task.FromResult(account);
        }

        public Task<ICollection<BankingAccount>> GetMyAccountsAsync(string UserId)
        {
            var accounts = _context.Accounts.Where(a => a.UserId == UserId).ToList();
            return Task.FromResult<ICollection<BankingAccount>>(accounts);
        }

        private async Task<bool> HasTwoOrMoreAccountsOfTypeAsync(string userId, string type)
        {
            return await _context.Accounts
                .Where(a => a.UserId == userId && a.AccountType == type)
                .CountAsync() >= 2;
        }
    }
}
