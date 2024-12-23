using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class AccountPermission
    {
        public int UserId { get; set; }
        public Permission PermissionId { get; set; }
    }
}
