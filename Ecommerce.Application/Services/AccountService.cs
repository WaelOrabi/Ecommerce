using Application.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Extensions;
using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;
namespace Ecommerce.Application.Services
{
    public class AccountService(IUnitOfWork unitOfWork, TokenProvider tokenProvider, IMapper mapper) : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly TokenProvider _tokenProvider = tokenProvider;
        private readonly IMapper _mapper = mapper;

        public async Task<AccountResponse> Add(AccountRequest accountRequest)
        {


            Account account = _mapper.Map<Account>(accountRequest);
            var result = await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AccountResponse>(result);
        }
        public async Task<AccountResponse?> Update(AccountRequest accountRequest, int id)
        {
            var account = await unitOfWork.AccountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }


            account = _mapper.Map<Account>(accountRequest);
            var result = _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AccountResponse>(result); ;
        }
        public async Task<int> Delete(int id)
        {

            await _unitOfWork.AccountRepository.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var result = await _unitOfWork.AccountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountResponse>>(result);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var result = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            return _mapper.Map<AccountResponse>(result);
        }

        public async Task<string> Auth(AuthAccountRequestDTO authAccount)
        {
            var account = await _unitOfWork.AccountRepository.Auth(authAccount);
            if (account == null)
            {
                return "Account not found";
            }


            return _tokenProvider.Create(account);
        }

    }
}
