using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authentication.GetRefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesKeys> _localizer;
        #endregion
        #region Constructors
        public RefreshTokenValidator(IStringLocalizer<SharedResourcesKeys> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}
