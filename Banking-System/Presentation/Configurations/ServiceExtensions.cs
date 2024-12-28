using Banking_System.Application.Auth.Interfaces;
using Banking_System.Application.Auth.UseCases;
using Banking_System.Application.Account.Interfaces;
using Banking_System.Application.Account.UseCases;
using Banking_System.Application.Transaction.Interfaces;
using Banking_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Banking_System.Core.Interfaces;
using Banking_System.Core.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBankingTransaction, BankingTransactions>();
        services.AddScoped<IInterestService, InterestService>();
        services.AddScoped<GenerateJwtTokenUseCase>();

        return services;
    }

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BankingDataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<BankingDataContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
