using FluentValidation;

namespace BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Request
{
    public class CreateNewSeasonRefRequest
    {
        public string Name { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateNewSeasonRefRequestValidator : AbstractValidator<CreateNewSeasonRefRequest>
    {
        public CreateNewSeasonRefRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
