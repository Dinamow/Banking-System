using Banking_System.Core.Interfaces;
using Banking_System.Core.Models;
using System;

namespace Banking_System.Core.Services
{
    public class InterestService : IInterestService
    {
        private const decimal AnnualInterestRate = 0.12m; // 12% annual interest
        private const int DaysInYear = 365;

        public void CalculateDailyInterest(BankingAccount account)
        {
            if (account.AccountType != "SavingAccount")
                return;

            DateTime today = DateTime.UtcNow.Date;
            if (account.LastInterestCalculationDate >= today)
                return;

            // Calculate the number of days since the last calculation
            int daysSinceLastCalculation = (today - account.LastInterestCalculationDate).Days;

            // Calculate daily interest rate
            decimal dailyInterestRate = AnnualInterestRate / DaysInYear;

            // Accumulate interest
            decimal interest = account.Balance * dailyInterestRate * daysSinceLastCalculation;
            account.AccumulatedInterest += interest;

            // Update the last calculation date
            account.LastInterestCalculationDate = today;
        }
    }
}
