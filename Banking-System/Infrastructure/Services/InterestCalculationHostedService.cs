using Banking_System.Core.Interfaces;
using Banking_System.Infrastructure.Data;

namespace Banking_System.Infrastructure.Services
{
    public class InterestCalculationHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public InterestCalculationHostedService(IServiceProvider serviceProvider, Timer timer)
        {
            _serviceProvider = serviceProvider;
            _timer = timer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Schedule the task to run daily at midnight
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BankingDataContext>();
                var interestService = scope.ServiceProvider.GetRequiredService<IInterestService>();

                var savingAccounts = dbContext.Accounts
                    .Where(account => account.AccountType == "SavingAccount")
                    .ToList();

                foreach (var account in savingAccounts)
                {
                    interestService.CalculateDailyInterest(account);
                }

                dbContext.SaveChanges();
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose() { _timer?.Dispose(); }
    }
}