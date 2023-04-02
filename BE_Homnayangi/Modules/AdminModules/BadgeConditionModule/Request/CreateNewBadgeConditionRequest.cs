using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Request
{
    public class CreateNewBadgeConditionRequest
    {
        public int Accomplishments { get; set; }
        public int Orders { get; set; }
        public Guid BadgeId { get; set; }
    }
    public class CreateNeBadgeConditionRequestValidator : AbstractValidator<CreateNewBadgeConditionRequest>
    {
        public CreateNeBadgeConditionRequestValidator()
        {
            RuleFor(x => x.Accomplishments).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Orders).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
