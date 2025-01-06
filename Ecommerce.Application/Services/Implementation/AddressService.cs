using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Address;
using Ecommerce.Application.Resources;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class AddressService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer) : ResponseHandler(localizer), IAddressService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<SharedResources> _localizer = localizer;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Add(AddressRequest addressRequest)
        {


            Address address = _mapper.Map<Address>(addressRequest);
            await _unitOfWork.AddressRepository.AddAsync(address);
            await _unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");

        }

        public async Task<Response<string>> Update(AddressRequest addressRequest, int id)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
                return GenerateNotFoundResponse<string>();

            address = _mapper.Map(addressRequest, address);
            unitOfWork.AddressRepository.Update(address);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");

        }
        public async Task<Response<int>> Delete(int id)
        {

            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
                return GenerateNotFoundResponse<int>();
            await unitOfWork.AddressRepository.DeleteByIdAsync(address);
            await unitOfWork.CompleteAsync();
            return GenerateDeleteResponse(id);

        }

        public async Task<Response<IEnumerable<AddressResponse>>> GetAll()
        {
            var result = await unitOfWork.AddressRepository.GetAllAsync();
            var resultMapping = _mapper.Map<IEnumerable<AddressResponse>>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<AddressResponse>> GetById(int id)
        {
            var result = await unitOfWork.AddressRepository.GetByIdAsync(id);
            if (result == null)
                return GenerateNotFoundResponse<AddressResponse>();
            var resultMapping = _mapper.Map<AddressResponse>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<bool> IsAddressExist(int id)
        {

            var result = await _unitOfWork.AddressRepository.GetTableNoTracking().AnyAsync(x => x.Id.Equals(id));
            return result;

        }
    }
}
