using Banking_System.Application.Account.Interfaces;
using Banking_System.Application.Transaction.Interfaces;
using Banking_System.Core.Models;
using Banking_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class BankingTransactions : IBankingTransaction
{
    private readonly BankingDataContext _context;
    private readonly IAccountService _accountService;
    private const decimal OVER_DRAFT_LIMIT = 500;

    public BankingTransactions(BankingDataContext context, IAccountService accountService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    public async Task<BankingTransaction> CreateTransactionAsync(string UserId, int AccountId, string transactionType, decimal Amount, int TargetAccount)
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("Transaction amount must be greater than zero.");
        }

        var SourceAccount = await _accountService.GetAccountAsync(AccountId);
        var ToAccount = TargetAccount != 0 ? await _accountService.GetAccountAsync(TargetAccount) : SourceAccount;

        if (SourceAccount == null) throw new ArgumentException("Source account not found.");
        if (transactionType == "transfer" && ToAccount == null) throw new ArgumentException("Target account not found.");

        switch (transactionType)
        {
            case "deposit":
                HandleDeposit(SourceAccount, Amount);
                break;
            case "withdraw":
                await _accountService.GetMyAccountAsync(UserId, AccountId);
                if (!CanWithdraw(SourceAccount, Amount)) throw new InvalidOperationException("Insufficient funds.");
                HandleWithdraw(SourceAccount, Amount);
                break;
            case "transfer":
                await _accountService.GetMyAccountAsync(UserId, AccountId);
                if (!CanWithdraw(SourceAccount, Amount)) throw new InvalidOperationException("Insufficient funds.");
                HandleWithdraw(SourceAccount, Amount);
                HandleDeposit(ToAccount, Amount);
                break;
            default:
                throw new InvalidOperationException("Invalid Transaction Type");
        }
        _context.Update(SourceAccount);
        if (ToAccount != null && ToAccount != SourceAccount) _context.Update(ToAccount);
        return await CreateTransactionRecord(AccountId, transactionType, Amount, TargetAccount);
    }

    private bool CanWithdraw(BankingAccount account, decimal amount)
    {
        decimal overdraftLimit = account.AccountType == "SavingsAccount" ? 0 : OVER_DRAFT_LIMIT - account.OverDraftLimit;
        return account.Balance + overdraftLimit >= amount;
    }

    private async Task<BankingTransaction> CreateTransactionRecord(int AccountId, string transactionType, decimal Amount, int TargetAccount)
    {
        var transaction = new BankingTransaction
        {
            AccountId = AccountId,
            TransactionType = transactionType.ToString(),
            Amount = Amount,
            TargetAccount = TargetAccount != 0 ? TargetAccount : AccountId
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
    private void HandleWithdraw(BankingAccount account, decimal amount)
    {
        if (account.AccountType == "SavingsAccount")
        {
            account.Balance -= amount;
        }
        else
        {
            decimal overdraftLimit = OVER_DRAFT_LIMIT - account.OverDraftLimit;
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
            }
            else if (account.Balance + overdraftLimit >= amount)
            {
                account.OverDraftLimit += amount - account.Balance;
                account.Balance = 0;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds.");
            }
        }
    }
    private void HandleDeposit(BankingAccount account, decimal amount)
    {
        if (account.OverDraftLimit > 0)
        {
            if (amount >= account.OverDraftLimit)
            {
                amount -= account.OverDraftLimit;
                account.OverDraftLimit = 0;
            }
            else
            {
                account.OverDraftLimit -= amount;
                amount = 0;
            }
        }
        account.Balance += amount;
    }
}
