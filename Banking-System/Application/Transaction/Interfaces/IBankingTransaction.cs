using Banking_System.Core.Models;

namespace Banking_System.Application.Transaction.Interfaces
{
    public interface IBankingTransaction
    {
        Task<BankingTransaction> CreateTransactionAsync(string UserId, int AccountId, string TransactionType, decimal Amount, int TargetAccount);
    }
}
