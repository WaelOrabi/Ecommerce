using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.AuthAccount;
using Ecommerce.Domain.DTO.RequestsDTO.Account;
using Ecommerce.Domain.DTO.Responses;

namespace Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AccountResponse>> GetById(int id);
        Task<Response<IEnumerable<AccountResponse>>> GetAll();
        Task<Response<string>> Add(AccountRequest accountRequest);
        Task<Response<string>> Auth(AuthAccountRequest authAccount);
        Task<Response<string>> Update(AccountRequest accountRequest, int id);
        Task<Response<int>> Delete(int id);
        Task<bool> IsAccountExist(int id);

    }
}
