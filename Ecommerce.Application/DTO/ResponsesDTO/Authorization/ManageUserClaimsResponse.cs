namespace Ecommerce.Application.DTO.ResponsesDTO.Authorization
{
    public class ManageUserClaimsResponse
    {
        public int UserId { get; set; }
        public List<UserClaimsResponse> userClaims { get; set; } = new();
    }
    public class UserClaimsResponse
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
