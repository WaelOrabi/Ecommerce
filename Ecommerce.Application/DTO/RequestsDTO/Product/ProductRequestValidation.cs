using Application.Services.Interfaces;
using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Product
{
    public class ProductRequestValidation : AbstractValidator<ProductRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryService;
        public ProductRequestValidation(IStringLocalizer<SharedResources> localizer, ICategoryService categoryService)
        {
            _localizer = localizer;
            _categoryService = categoryService;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();
        }

        public void ApplayValidationsRules()
        {


            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .GreaterThan(0);

            RuleFor(x => x.CreateAt)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(_localizer[SharedResourcesKeys.InvalidCreationDate]);


        }

        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.CategoryId).MustAsync(async (key, CancellationToken) => await _categoryService.IsCategoryExist(key)).WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
        }
    }
}
