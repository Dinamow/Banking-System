using AutoMapper;
using Banking_System.Application.Auth.DTOs;
using Banking_System.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Banking_System.Application.Auth.Mapping
{
    public class AuthMapping : Profile
    {
        public AuthMapping()
        {
            CreateMap<IdentityUser, AuthResponseDTO>();
        }
    }
}
