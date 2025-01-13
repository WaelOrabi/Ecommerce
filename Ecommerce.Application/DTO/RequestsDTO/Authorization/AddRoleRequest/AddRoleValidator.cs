using Ecommerce.Application.Resources;
using Ecommerce.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Authorization.AddRoleRequest
{
    public class AddRoleValidator : AbstractValidator<AddRoleRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesKeys> _localizer;
        private readonly RoleManager<Role> _roleManager;
        #endregion
        #region Constructors
        public AddRoleValidator(IStringLocalizer<SharedResourcesKeys> localizer, RoleManager<Role> roleManager)
        {
            _localizer = localizer;
            _roleManager = roleManager;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameRole).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }
        public async void ApplyCustomValidationsRules()
        {

            RuleFor(x => x.NameRole).MustAsync(async (Key, CancellationToken) => await _roleManager.RoleExistsAsync(Key))
                               .WithMessage(_localizer[SharedResourcesKeys.IsExist]);
        }
        #endregion
    }
}
