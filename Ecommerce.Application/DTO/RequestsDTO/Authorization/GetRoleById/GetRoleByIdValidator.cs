using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authorization.GetRoleById
{
    public class GetRoleByIdValidator : AbstractValidator<GetByIdRoleRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion
        #region Constructors
        public GetRoleByIdValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            ApplayValidationRules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

        }

        #endregion
    }
}
