using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Authorization
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class CheckPermissionAttribute:Attribute
    {
        public Permission Permission { get; }
        public CheckPermissionAttribute(Permission permission)
        {
            Permission = permission;
        }
    }
}
