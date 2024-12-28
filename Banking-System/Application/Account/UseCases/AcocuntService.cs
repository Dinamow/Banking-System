using Banking_System.Application.Account.Interfaces;
using Banking_System.Core.Models;
using Banking_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banking_System.Application.Account.UseCases
{
    public class AccountService : IAccountService
    {
        private readonly BankingDataContext _context;
        private static readonly Random _random = new Random();

        public AccountService(BankingDataContext context)
        {
            _context = context;
        }

        public async Task<bool> AccountExistsAsync(int id)
        {
            return await _context.Accounts.AnyAsync(a => a.Id == id);
        }

        public async Task<BankingAccount> CreateAccountAsync(string userId, string type)
        {
            if (type != "SavingAccount" && type != "CheckingAccount")
            {
                throw new ArgumentException("Invalid account type. Must be 'SavingAccount' or 'CheckingAccount'.");
            }

            if (await HasTwoOrMoreAccountsOfTypeAsync(userId, type))
            {
                throw new InvalidOperationException($"User already has two or more {type} accounts.");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var account = new BankingAccount
            {
                AccountNumber = await GenerateUniqueAccountNumberAsync(),
                AccountType = type,
                Balance = 0,
                OverDraftLimit = 0,
                IntrestRate = 0,
                AccumulatedInterest = 0,
                LastInterestCalculationDate = DateTime.UtcNow,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                UserId = userId,
                User = user
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<BankingAccount> GetAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found.");
            }
            return account;
        }

        public async Task<ICollection<BankingAccount>> GetMyAccountsAsync(string userId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<BankingAccount> GetMyAccountAsync(string userId, int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null || account.UserId != userId)
            {
                throw new InvalidOperationException("UnAuthrized");
            }
            return account;
        }

        private async Task<bool> HasTwoOrMoreAccountsOfTypeAsync(string userId, string type)
        {
            return await _context.Accounts
                .CountAsync(a => a.UserId == userId && a.AccountType == type) >= 2;
        }

        private async Task<string> GenerateUniqueAccountNumberAsync()
        {
            string accountNumber;
            do
            {
                accountNumber = $"ACCT-{_random.Next(10000000, 99999999)}";
            }
            while (await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber));

            return accountNumber;
        }
    }
}
