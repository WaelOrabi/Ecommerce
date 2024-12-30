﻿namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class AddressRequestDTO
    {

        public string Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
