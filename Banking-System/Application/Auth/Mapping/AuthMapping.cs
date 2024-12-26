using AutoMapper;
using Banking_System.Application.Auth.DTOs;
using Banking_System.Core.Models;

namespace Banking_System.Application.Auth.Mapping
{
    public class AuthMapping : Profile
    {
        public AuthMapping()
        {
            CreateMap<Account, AuthResponseDTO>();
        }
    }
}
