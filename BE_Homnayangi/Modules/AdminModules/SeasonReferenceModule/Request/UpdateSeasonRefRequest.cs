using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Request
{
    public class UpdateSeasonRefRequest
    {
        public Guid SeasonRefId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateSeasonRefRequestValidator : AbstractValidator<UpdateSeasonRefRequest>
    {
        public UpdateSeasonRefRequestValidator()
        {
            RuleFor(x => x.SeasonRefId).NotEmpty().NotNull();
        }
    }
}
