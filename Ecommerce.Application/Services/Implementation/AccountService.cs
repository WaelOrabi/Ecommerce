using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.AuthAccount;
using Ecommerce.Application.Extensions;
using Ecommerce.Application.Resources;
using Ecommerce.Domain.DTO.RequestsDTO.Account;
using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class AccountService(IUnitOfWork unitOfWork, TokenProvider tokenProvider, IMapper mapper, IStringLocalizer<SharedResources> localizer) : ResponseHandler(localizer), IAccountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly TokenProvider _tokenProvider = tokenProvider;
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public async Task<Response<string>> Add(AccountRequest accountRequest)
        {


            Account account = _mapper.Map<Account>(accountRequest);
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CompleteAsync();
            return GenerateCreatedResponse("");
        }
        public async Task<Response<string>> Update(AccountRequest accountRequest, int id)
        {
            var account = await unitOfWork.AccountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return GenerateNotFoundResponse<string>();
            }


            account = _mapper.Map(accountRequest, account);
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("Update Successfully");
        }
        public async Task<Response<int>> Delete(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (account == null)
                return GenerateNotFoundResponse<int>();
            await _unitOfWork.AccountRepository.DeleteByIdAsync(account);
            await _unitOfWork.CompleteAsync();
            return GenerateDeleteResponse<int>(id);
        }

        public async Task<Response<IEnumerable<AccountResponse>>> GetAll()
        {
            var result = await _unitOfWork.AccountRepository.GetTableNoTracking().Include(x => x.Address)
                                                                           .Include(x => x.Role)
                                                                           .Include(x => x.Orders).ThenInclude(x => x.OrderProducts)
                                                                           .ToListAsync();
            var resultMapping = _mapper.Map<IEnumerable<AccountResponse>>(result);
            return GenerateSuccessResponse<IEnumerable<AccountResponse>>(resultMapping);
        }

        public async Task<Response<AccountResponse>> GetById(int id)
        {
            var result = await _unitOfWork.AccountRepository.GetTableNoTracking().Include(x => x.Address)
                                                                         .Include(x => x.Role)
                                                                         .Include(x => x.Orders).ThenInclude(x => x.OrderProducts).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
                return GenerateNotFoundResponse<AccountResponse>();
            var resultMapping = _mapper.Map<AccountResponse>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<string>> Auth(AuthAccountRequest authAccount)
        {
            var account = await _unitOfWork.AccountRepository.Auth(authAccount.Email, authAccount.Password);
            if (account == null)
            {
                return GenerateNotFoundResponse<string>();
            }


            var accessToken = _tokenProvider.Create(account);
            return GenerateCreatedResponse<string>(accessToken);
        }

        public async Task<bool> IsAccountExist(int id)
        {
            var result = await _unitOfWork.AccountRepository.GetTableNoTracking().AnyAsync(x => x.Id.Equals(id));
            return result;
        }
    }
}
