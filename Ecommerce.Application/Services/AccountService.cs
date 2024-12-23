using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Application.Extensions;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Ecommerce.Application.Services
{
    public class AccountService(IUnitOfWork unitOfWork, TokenProvider tokenProvider) : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly TokenProvider _tokenProvider = tokenProvider;

        public async Task<Account> Add(AccountRequest accountRequest)
        {
            if (accountRequest == null) {
                throw new ArgumentNullException();
            }
            Account account = new Account { 
            Id = accountRequest.Id,
            FirstName = accountRequest.FirstName,
            LastName = accountRequest.LastName,
            Email= accountRequest.Email,
            PhoneNumber=accountRequest.PhoneNumber,
            BirthDate=accountRequest.BirthDate,
            Password=accountRequest.Password,
            RoleId=accountRequest.RoleId,
            AddressId=accountRequest.AddressId,
            };
           var result=await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<Account> Update(AccountRequest accountRequest)
        {
            if (accountRequest == null)
            {
                throw new ArgumentNullException();
            }
            Account account = new Account
            {
                Id = accountRequest.Id,
                FirstName = accountRequest.FirstName,
                LastName = accountRequest.LastName,
                Email = accountRequest.Email,
                PhoneNumber = accountRequest.PhoneNumber,
                BirthDate = accountRequest.BirthDate,
                Password = accountRequest.Password,
                RoleId = accountRequest.RoleId,
                AddressId = accountRequest.AddressId,
            };
            var result = _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<int> Delete(int id)
        {
         
            await _unitOfWork.AccountRepository.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _unitOfWork.AccountRepository.GetAllAsync();
        }

        public async Task<Account> GetById(int id)
        {
            return await _unitOfWork.AccountRepository.GetByIdAsync(id);
        }

        public async Task<string> Auth(AuthAccount authAccount)
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
