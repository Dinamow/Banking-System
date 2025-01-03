﻿using AutoMapper;
using Banking_System.Application.Auth.Mapping;
using Banking_System.Application.Account.Mapping;
using Banking_System.Application.Transaction.Mapping;

public static class AutoMapperConfig
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthMapping).Assembly);
        services.AddAutoMapper(typeof(AccountMapping).Assembly);
        services.AddAutoMapper(typeof(TransactionMapping).Assembly);
    }
}
