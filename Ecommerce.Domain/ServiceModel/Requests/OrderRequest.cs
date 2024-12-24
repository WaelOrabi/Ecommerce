using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class OrderRequest
    {
        public int AccountId { get; set; }
        public List<OrderProductRequest> OrderProducts { get; set; }
    }
}
