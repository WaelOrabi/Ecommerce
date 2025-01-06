using Application.Services.Interfaces;
using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Domain.DTO.RequestsDTO.Account
{
    public class AccountValidation : AbstractValidator<AccountRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IRoleService _roleService;
        private readonly IAddressService _addressService;
        public AccountValidation(IStringLocalizer<SharedResources> localizer, IRoleService roleService, IAddressService addressService)
        {
            _roleService = roleService;
            _addressService = addressService;
            _localizer = localizer;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }

        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.RoleId).MustAsync(async (key, CancellationToken) => await _roleService.IsRoleExist(key)).WithMessage(_localizer[SharedResourcesKeys.InvalidRole]);
            RuleFor(x => x.AddressId).MustAsync(async (key, CancellationToken) => await _addressService.IsAddressExist(key)).WithMessage(_localizer[SharedResourcesKeys.InvalidAddress]);

        }

        public void ApplayValidationsRules()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
               ;

            RuleFor(x => x.BirthDate)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
              .LessThan(new DateTime(2012, 12, 1)).WithMessage(_localizer[SharedResourcesKeys.InvalidBirthDate]);
            RuleFor(x => x.PhoneNumber)
                   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .MinimumLength(6).WithMessage(_localizer[SharedResourcesKeys.PasswordTooShort]);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PasswordsDoNotMatch]);




        }
    }
}
