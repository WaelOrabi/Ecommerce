using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.AuthAccount
{
    public class AuthAccountValidation : AbstractValidator<AuthAccountRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;


        public AuthAccountValidation(IStringLocalizer<SharedResources> localizer)
        {

            _localizer = localizer;
            ApplayValidationsRules();
        }
        public void ApplayValidationsRules()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

        }
    }
}
