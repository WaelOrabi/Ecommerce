namespace Ecommerce.Application.DTO.RequestsDTO.Authentication.GetRefreshToken
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
