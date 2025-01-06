using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Category
{
    public class CategoryValidation : AbstractValidator<CategoryRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;


        public CategoryValidation(IStringLocalizer<SharedResources> localizer)
        {

            _localizer = localizer;
            ApplayValidationsRules();
        }
        public void ApplayValidationsRules()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
        }
    }
}
