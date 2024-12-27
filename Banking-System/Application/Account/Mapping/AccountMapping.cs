using AutoMapper;
using Banking_System.Application.Account.DTOs;
using Banking_System.Core.Models;

namespace Banking_System.Application.Account.Mapping
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<BankingAccount, AccountDTO>();
        }
    }
}
