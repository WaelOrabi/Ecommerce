﻿using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Ecommerce.Domain.DTO.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
        public IEnumerable<UserReviewsResponse> Reviews { get; set; }

    }
    public class UserReviewsResponse
    {
        public string ProductName { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

    }
}
