using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [InverseProperty(nameof(UserRefreshToken.User))]
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public Cart Cart { get; set; }
    }
}
