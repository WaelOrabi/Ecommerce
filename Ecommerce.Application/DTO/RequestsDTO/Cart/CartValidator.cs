using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Cart
{
    public class CartValidation : AbstractValidator<CartRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        //private readonly IAccountService _accountService;

        public CartValidation(IStringLocalizer<SharedResources> localizer)//, IAccountService accountService)
        {
            //     _accountService = accountService;
            _localizer = localizer;

            ApplayCustomValidationsRules();

        }

        public void ApplayCustomValidationsRules()
        {
            //       RuleFor(x => x.AccountId).MustAsync(async (key, CancellationToken) => await _accountService.IsAccountExist(key)).WithMessage(_localizer[SharedResourcesKeys.NotFound]);

        }


    }
}
