namespace Ecommerce.Application.DTO.ResponsesDTO.Authorization
{
    public class ManageUserRolesResponse
    {

        public int UserId { get; set; }
        public List<RoleUserResponse> Roles { get; set; } = new List<RoleUserResponse>();
    }
    public class RoleUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
