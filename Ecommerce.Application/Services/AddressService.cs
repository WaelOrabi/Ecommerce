using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;

namespace Ecommerce.Application.Services
{
    public class AddressService(IUnitOfWork unitOfWork) : IAddressService
    {
        public async Task<Address> Add(AddressRequest addressRequest)
        {
            if (addressRequest == null) {
                throw new ArgumentNullException();
            }
            Address address = new Address { 
            Id = addressRequest.Id,
            Street = addressRequest.Street,
            City = addressRequest.City,
            PostalCode = addressRequest.PostalCode,
            Country = addressRequest.Country,
            State = addressRequest.State,
            
            };
            var result= await unitOfWork.AddressRepository.AddAsync(address);
            await unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<Address> Update(AddressRequest addressRequest)
        {
            if (addressRequest == null)
            {
                throw new ArgumentNullException();
            }
            Address address = new Address
            {
                Id = addressRequest.Id,
                Street = addressRequest.Street,
                City = addressRequest.City,
                PostalCode = addressRequest.PostalCode,
                Country = addressRequest.Country,
                State = addressRequest.State,

            };
            var result =  unitOfWork.AddressRepository.Update(address);
            await unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<int> Delete(int id)
        {
      
      
            await unitOfWork.AddressRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await unitOfWork.AddressRepository.GetAllAsync();
        }

        public async Task<Address> GetById(int id)
        {
            return await unitOfWork.AddressRepository.GetByIdAsync(id);
        }

    }
}
