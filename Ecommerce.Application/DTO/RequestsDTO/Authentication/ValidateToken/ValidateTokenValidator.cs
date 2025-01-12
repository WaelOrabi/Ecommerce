using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authentication.ValidateToken
{
    public class ValidateTokenValidator : AbstractValidator<ValidateTokenRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesKeys> _localizer;
        #endregion
        #region Constructors
        public ValidateTokenValidator(IStringLocalizer<SharedResourcesKeys> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }
        #endregion
    }
}
