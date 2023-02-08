using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.CategoryModule.Request
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
