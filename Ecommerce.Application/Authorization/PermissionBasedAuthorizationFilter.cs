using Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Authorization
{
    public class PermissionBasedAuthorizationFilter(IUnitOfWork unitOfWork) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attibute =(CheckPermissionAttribute) context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
            if(attibute != null)
            {
                var claimIdentity=context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                    context.Result = new ForbidResult();
                else
                {
                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var hasPermission = await unitOfWork.AccountRepository.HasPermission(userId,attibute.Permission);
                    if (!hasPermission)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}
