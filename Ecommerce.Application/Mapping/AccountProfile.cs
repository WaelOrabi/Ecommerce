using AutoMapper;
using Ecommerce.Domain.DTO.RequestsDTO.Account;
using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<AccountRequest, Account>();
        }
    }
}
