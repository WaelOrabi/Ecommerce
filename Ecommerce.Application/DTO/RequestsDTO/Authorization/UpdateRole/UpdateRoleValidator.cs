using Ecommerce.Application.Resources;
using Ecommerce.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly RoleManager<Role> _roleManager;
        #endregion
        #region Constructors
        public UpdateRoleValidator(IStringLocalizer<SharedResources> stringLocalizer, RoleManager<Role> roleManager)
        {
            _stringLocalizer = stringLocalizer;
            _roleManager = roleManager;
            ApplayValidationRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                    .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
        public async void ApplyCustomValidationsRules()
        {

            RuleFor(x => x.Name).MustAsync(async (Key, CancellationToken) => await _roleManager.RoleExistsAsync(Key))
                               .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);
        }
        #endregion
    }
}
