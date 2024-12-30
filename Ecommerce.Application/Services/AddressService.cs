using Application.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Application.Services
{
    public class AddressService(IUnitOfWork unitOfWork, IMapper mapper) : IAddressService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<AddressResponse> Add(AddressRequestDTO addressRequest)
        {


            Address address = _mapper.Map<Address>(addressRequest);
            var result = await unitOfWork.AddressRepository.AddAsync(address);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<AddressResponse>(result);
        }

        public async Task<AddressResponse?> Update(AddressRequestDTO addressRequest, int id)
        {
            var address = await unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
                return null;

            address = _mapper.Map<Address>(addressRequest);
            var result = unitOfWork.AddressRepository.Update(address);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<AddressResponse>(result);
        }
        public async Task<int> Delete(int id)
        {


            await unitOfWork.AddressRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<AddressResponse>> GetAll()
        {
            var result = await unitOfWork.AddressRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressResponse>>(result);
        }

        public async Task<AddressResponse> GetById(int id)
        {
            var result = await unitOfWork.AddressRepository.GetByIdAsync(id);
            return _mapper.Map<AddressResponse>(result);
        }

    }
}
