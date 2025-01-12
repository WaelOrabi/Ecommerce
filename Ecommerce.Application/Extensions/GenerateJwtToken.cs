//using System.Security.Claims;
//using System.Text;
//using Ecommerce.Domain.Entities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.JsonWebTokens;
//using Microsoft.IdentityModel.Tokens;


//namespace Ecommerce.Application.Extensions
//{
//    public  sealed  class TokenProvider(IConfiguration configuration)
//    {
//        public  string Create( Account account)
//        {

//            string secretKey = configuration["Jwt:SigningKey"];
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var tokenDescription = new SecurityTokenDescriptor { 
//            Subject=new ClaimsIdentity([
//                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
//                new Claim(ClaimTypes.Email, account.Email),
//                    new Claim(ClaimTypes.Name, account.FirstName+account.LastName),
//                    new Claim(ClaimTypes.Role,account.Role.Name),
//               ]),
//            Expires=DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:Lifetime")),
//            SigningCredentials = credentials,
//                Issuer = configuration["Jwt:Issuer"],
//                Audience = configuration["Jwt:Audience"]
//            };

//            var handler = new JsonWebTokenHandler();
//            string token = handler.CreateToken(tokenDescription);
//            return token;
//        }

//    }
//}
