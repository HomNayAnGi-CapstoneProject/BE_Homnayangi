using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.CategoryModule.Request
{
    public class CreateRegionRequest
    {
        public string RegionName { get; set; }
    }
    public class CreateRegionRequestValidator : AbstractValidator<CreateRegionRequest>
    {
        public CreateRegionRequestValidator()
        {
            RuleFor(x => x.RegionName).NotEmpty().NotNull();
        }
    }
}
