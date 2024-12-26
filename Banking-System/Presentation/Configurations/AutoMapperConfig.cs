using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Banking_System.Application.Auth.Mapping;

public static class AutoMapperConfig
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthMapping).Assembly);
    }
}
