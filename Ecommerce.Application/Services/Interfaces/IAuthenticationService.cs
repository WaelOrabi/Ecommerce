using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.GetRefreshToken;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.Sigin;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.ValidateToken;
using Ecommerce.Application.DTO.ResponsesDTO;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<Response<SigInResponse>> SigIn(SigInRequest siginRequest);
        public Task<Response<SigInResponse>> GetRefreshToken(RefreshTokenRequest refreshTokenRequest);
        public Task<Response<string>> ValidateToken(ValidateTokenRequest validateTokenRequest);
    }
}
