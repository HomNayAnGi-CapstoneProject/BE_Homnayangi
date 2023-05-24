using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.CategoryModule.Request
{
    public class UpdateRegionRequest
    {
        public Guid RegionId { get; set; }
        public string RegionName { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateRegionRequestValidator : AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.RegionId).NotEmpty().NotNull();
        }
    }
}
