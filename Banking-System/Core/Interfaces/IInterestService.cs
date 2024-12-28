using Banking_System.Core.Models;

namespace Banking_System.Core.Interfaces
{
    public interface IInterestService
    {
        void CalculateDailyInterest(BankingAccount account);
    }
}
