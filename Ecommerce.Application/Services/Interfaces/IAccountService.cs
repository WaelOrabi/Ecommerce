using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> GetById(int id);
        Task<IEnumerable<AccountResponse>> GetAll();
        Task<AccountResponse> Add(AccountRequest accountRequest);
        Task<string> Auth(AuthAccountRequestDTO authAccount);
        Task<AccountResponse?> Update(AccountRequest accountRequest, int id);
        Task<int> Delete(int id);

    }
}
