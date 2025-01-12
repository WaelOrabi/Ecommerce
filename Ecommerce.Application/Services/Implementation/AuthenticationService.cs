
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.GetRefreshToken;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.Sigin;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.ValidateToken;
using Ecommerce.Application.DTO.ResponsesDTO;
using Ecommerce.Application.Resources;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Domain.Helpers;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace Ecommerce.Application.Services.Implementation
{
    public class AuthenticationService : ResponseHandler, IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _sigInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(UserManager<User> userManager, IStringLocalizer<SharedResources> localizer, IMapper mapper, SignInManager<User> sigInManager, JwtSettings jwtSettings, IUnitOfWork unitOfWork) : base(localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
            _mapper = mapper;
            _sigInManager = sigInManager;
            _jwtSettings = jwtSettings;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<SigInResponse>> SigIn(SigInRequest siginRequest)
        {
            var user = await _userManager.FindByEmailAsync(siginRequest.Email);
            if (user == null)
                return GenerateBadRequestResponse<SigInResponse>(_localizer[SharedResourcesKeys.EmailIsNotExist]);
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return GenerateBadRequestResponse<SigInResponse>(_localizer[SharedResourcesKeys.FailedToUpdateEmailConfirmed]);
                }
            }
            var signInResult = await _sigInManager.CheckPasswordSignInAsync(user, siginRequest.Password, false);
            if (!signInResult.Succeeded)
                return GenerateBadRequestResponse<SigInResponse>(_localizer[SharedResourcesKeys.PasswordNotCorrect]);
            var result = await ReturnSigInResponse(user);
            return GenerateSuccessResponse<SigInResponse>(result);
        }
        private async Task<SigInResponse> ReturnSigInResponse(User user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = user.UserName,
                TokenString = GenerateRefreshToken()
            };
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id,
            };
            await _unitOfWork.UserRefreshTokenRepository.AddAsync(userRefreshToken);
            await _unitOfWork.CompleteAsync();
            return new SigInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id),user.Id.ToString())
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var jwtToken = new JwtSecurityToken(
                               _jwtSettings.Issuer,
                               _jwtSettings.Audience,
                                claims,
                              expires: DateTime.Now.AddDays(_jwtSettings.Lifetime),
                              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)), SecurityAlgorithms.HmacSha256Signature)
                              );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }

        public async Task<Response<SigInResponse>> GetRefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            // Read Token
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenRequest.AccessToken);

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                return GenerateBadRequestResponse<SigInResponse>("AlogrithmIsWrong");
            if (jwtToken.ValidTo > DateTime.UtcNow)
                return GenerateBadRequestResponse<SigInResponse>("Token Is Not Expired");
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _unitOfWork.UserRefreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.RefreshToken == refreshTokenRequest.RefreshToken &&
                                                                                                                        x.Token == refreshTokenRequest.AccessToken &&
                                                                                                                        x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
                return GenerateBadRequestResponse<SigInResponse>("Refresh Token Is Not Found");
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                _unitOfWork.UserRefreshTokenRepository.Update(userRefreshToken);
                return GenerateBadRequestResponse<SigInResponse>("Refresh Token Is Expired");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return GenerateNotFoundResponse<SigInResponse>();

            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new SigInResponse();
            response.AccessToken = newToken;
            response.RefreshToken = new RefreshToken();

            response.RefreshToken.UserName = user.UserName;
            response.RefreshToken.TokenString = userRefreshToken.RefreshToken;
            response.RefreshToken.ExpireAt = userRefreshToken.ExpiryDate;

            userRefreshToken.Token = newToken;
            _unitOfWork.UserRefreshTokenRepository.Update(userRefreshToken);
            return GenerateSuccessResponse(response);
        }

        public async Task<Response<string>> ValidateToken(ValidateTokenRequest validateTokenRequest)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,

            };
            var validator = handler.ValidateToken(validateTokenRequest.Token, parameters, out SecurityToken validatedToken);
            if (validator == null)
                return GenerateBadRequestResponse<string>("Invalid Token");
            return GenerateSuccessResponse("Not Expired");

        }
    }
}
