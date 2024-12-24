using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class OrderProductRequest
    {
        public int ProductId { get; set;}
        public int Quantity {  get; set;}
    }
}
