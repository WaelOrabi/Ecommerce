using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Address
{
    public class AddressValidation : AbstractValidator<AddressRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;


        public AddressValidation(IStringLocalizer<SharedResources> localizer)
        {

            _localizer = localizer;
            ApplayValidationsRules();


        }



        public void ApplayValidationsRules()
        {
            RuleFor(x => x.Street)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])



            RuleFor(x => x.City)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);





        }
    }
}
