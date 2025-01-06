using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Role
{
    public class RoleValidation : AbstractValidator<RoleRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;


        public RoleValidation(IStringLocalizer<SharedResources> localizer)
        {

            _localizer = localizer;
            ApplayValidationsRules();
        }
        public void ApplayValidationsRules()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
        }
    }
}
