using Ecommerce.Application.Resources;
using Ecommerce.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Order
{
    public class OrderValidation : AbstractValidator<OrderRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _accountService;

        public OrderValidation(IStringLocalizer<SharedResources> localizer)
        {
            //  _accountService = accountService;
            _localizer = localizer;

            ApplayCustomValidationsRules();

        }

        public void ApplayCustomValidationsRules()
        {
            //  RuleFor(x => x.AccountId).MustAsync(async (key, CancellationToken) => await _accountService.IsAccountExist(key)).WithMessage(_localizer[SharedResourcesKeys.NotFound]);

        }


    }
}
