using Application.Services.Interfaces;
using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.OrderProduct
{
    public class OrderProductValidation : AbstractValidator<OrderProductRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IProductService _productService;

        public OrderProductValidation(IStringLocalizer<SharedResources> localizer, IProductService productService)
        {
            _productService = productService;
            _localizer = localizer;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }

        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.ProductId).MustAsync(async (key, CancellationToken) => await _productService.IsProductExist(key)).WithMessage(_localizer[SharedResourcesKeys.NotFound]);

        }

        public void ApplayValidationsRules()
        {
            RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
            .GreaterThan(0);

        }
    }
}
