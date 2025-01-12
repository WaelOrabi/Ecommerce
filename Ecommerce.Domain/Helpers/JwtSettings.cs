﻿namespace Ecommerce.Domain.Helpers
{



    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public string SigningKey { get; set; }
        public int RefreshTokenExpireDate { get; set; }
    }




}
