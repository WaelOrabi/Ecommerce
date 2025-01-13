using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authorization.ManageUserRoles
{
    public class ManageUserRolesValidator : AbstractValidator<ManageUserRolesRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        #region Constructors
        public ManageUserRolesValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;

            ApplayValidationsRules();


        }


        #endregion

        #region Actions
        public void ApplayValidationsRules()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


        }

        #endregion
    }
}
