using FluentValidation;

namespace BE_Homnayangi.Modules.UnitModule.Request
{
    public class CreateUnitRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateUnitRequestValidator : AbstractValidator<CreateUnitRequest>
    {
        public CreateUnitRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
