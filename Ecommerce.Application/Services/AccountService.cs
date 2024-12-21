using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class AccountService(IUnitOfWork unitOfWork) : IAccountService
    {
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
            RoleId=accountRequest.RoleId,
            AddressId=accountRequest.AddressId,
            };
           var result=await unitOfWork.AccountRepository.AddAsync(account);
            await unitOfWork.CompleteAsync();
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
                RoleId = accountRequest.RoleId,
                AddressId = accountRequest.AddressId,
            };
            var result =  unitOfWork.AccountRepository.Update(account);
            await unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<int> Delete(int id)
        {
         
            await unitOfWork.AccountRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await unitOfWork.AccountRepository.GetAllAsync();
        }

        public async Task<Account> GetById(int id)
        {
            return await unitOfWork.AccountRepository.GetByIdAsync(id);
        }

 
    }
}
