using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authentication.Sigin
{
    public class SigInValidator : AbstractValidator<SigInRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesKeys> _localizer;
        #endregion
        #region Constructors
        public SigInValidator(IStringLocalizer<SharedResourcesKeys> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}
