using AutoMapper;
using Banking_System.Application.Transaction.DTOs;
using Banking_System.Core.Models;

namespace Banking_System.Application.Transaction.Mapping
{
    public class TransactionMapping : Profile
    {
        public TransactionMapping()
        {
            CreateMap<BankingTransaction, TransactionDTO>();
        }
    }
}
