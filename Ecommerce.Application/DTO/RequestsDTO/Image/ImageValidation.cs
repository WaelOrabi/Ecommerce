using Ecommerce.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.DTO.RequestsDTO.Image
{
    public class ImageValidation : AbstractValidator<ImageRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ImageValidation(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;

            ApplayValidationsRules();

        }

        public void ApplayValidationsRules()
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            RuleFor(x => x.File)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .Must(image =>
                {
                    if (image == null) return false;
                    var extension = Path.GetExtension(image.FileName);
                    return allowedExtensions.Contains(extension.ToLower());
                }).WithMessage(_localizer[SharedResourcesKeys.InvalidFileExtension])
                .Must(image =>
                {
                    if (image == null) return false;
                    if (image.Length > 10485760)
                        return false;
                    return true;
                }).WithMessage(_localizer[SharedResourcesKeys.FileSizeIsGreaterThan10MB]);
        }

    }
}
