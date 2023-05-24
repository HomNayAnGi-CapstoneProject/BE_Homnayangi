using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.CookingMethodModule.Request
{
    public class UpdateCookingMethodRequest
    {
        public Guid CookingMethodId { get; set; }
        public string CookingMethodName { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateCookingMethodRequestValidator : AbstractValidator<UpdateCookingMethodRequest>
    {
        public UpdateCookingMethodRequestValidator()
        {
            RuleFor(x => x.CookingMethodId).NotEmpty().NotNull();
        }
    }
}
