using AutoMapper;
using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

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
