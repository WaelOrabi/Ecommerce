namespace Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateUserClaims
{

    public class UpdateUserClaimsRequest
    {
        public int UserId { get; set; }
        public List<UserClaimsRequest> userClaims { get; set; } = new();
    }
    public class UserClaimsRequest
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
