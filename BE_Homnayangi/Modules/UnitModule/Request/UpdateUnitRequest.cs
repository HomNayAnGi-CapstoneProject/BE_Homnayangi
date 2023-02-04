using BE_Homnayangi.Modules.TypeModule.DTO;
using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.UnitModule.Request
{
    public class UpdateUnitRequest
    {
        public Guid UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateUnitRequestValidator : AbstractValidator<UpdateUnitRequest>
    {
        public UpdateUnitRequestValidator()
        {
            RuleFor(x => x.UnitId).NotEmpty().NotNull();
        }
    }
}
