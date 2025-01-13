namespace Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateUserRoles
{
    public class UpdateUserRolesRequest
    {
        public int UserId { get; set; }
        public List<RoleUserRequest> Roles { get; set; } = new List<RoleUserRequest>();
    }
    public class RoleUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
