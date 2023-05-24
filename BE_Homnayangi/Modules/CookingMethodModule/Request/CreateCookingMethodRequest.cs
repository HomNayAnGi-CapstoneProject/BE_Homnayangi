using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.CookingMethodModule.Request
{
    public class CreateCookingMethodRequest
    {
        public string CookingMethodName { get; set; }
    }
    public class CreateCookingMethodRequestValidator : AbstractValidator<CreateCookingMethodRequest>
    {
        public CreateCookingMethodRequestValidator()
        {
            RuleFor(x => x.CookingMethodName).NotEmpty().NotNull();
        }
    }
}
