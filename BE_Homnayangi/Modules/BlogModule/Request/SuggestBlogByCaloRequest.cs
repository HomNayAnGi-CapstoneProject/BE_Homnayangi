using FluentValidation;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class SuggestBlogByCaloRequest
    {
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public bool IsLoseWeight { get; set; }
    }
    public class SuggestBlogByCaloRequestValidator : AbstractValidator<SuggestBlogByCaloRequest>
    {
        public SuggestBlogByCaloRequestValidator()
        {
            RuleFor(x => x.Age).NotEmpty().NotNull();
            RuleFor(x => x.IsMale).NotEmpty().NotNull();
            RuleFor(x => x.IsLoseWeight).NotEmpty().NotNull();
        }
    }
}
