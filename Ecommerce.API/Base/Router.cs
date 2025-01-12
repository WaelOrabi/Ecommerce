namespace Ecommerce.API.Base
{
    public class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Role = $"{root}/{version}/";
        public static class AccountRouting
        {
            public const string prefix = Role + "Account/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update/{id}";
            public const string Delete = prefix + "{id}";
            public const string Auth = prefix + "Auth";
        }
        public static class ApplicationUserRouting
        {
            public const string prefix = Role + "User/";
            public const string List = prefix + "List";
            public const string ListPagination = prefix + "Paginate";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
            public const string Auth = prefix + "Auth";
        }
        public static class AuthenticationRouting
        {
            public const string prefix = Role + "Authentication/";
            public const string SigIn = prefix + "SigIn";
            public const string RefreshToken = prefix + "Refresh-Token";
            public const string ValidateToken = prefix + "Validate-Token";

        }
        public static class AddressRouting
        {
            public const string prefix = Role + "Address/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
        }
        public static class CartRouting
        {
            public const string prefix = Role + "Cart/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
        }
        public static class CategoryRouting
        {
            public const string prefix = Role + "Category/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
        }
        public static class OrderRouting
        {
            public const string prefix = Role + "Order/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
        }
        public static class ProductRouting
        {
            public const string prefix = Role + "Product/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
        }
        public static class RoleRouting
        {
            public const string prefix = Role + "Role/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "{id}";
            public const string Create = prefix + "Create";
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "{id}";
        }
    }
}
