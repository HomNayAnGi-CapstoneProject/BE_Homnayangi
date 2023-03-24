using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request
{
    public class UpdateBadgeConditionRequest
    {
        public Guid BadgeConditionId { get; set; }
        public int? Accomplishments { get; set; }
        public int? Orders { get; set; }
        public Guid BadgeId { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateBadgeConditionRequestValidator : AbstractValidator<UpdateBadgeConditionRequest>
    {
        public UpdateBadgeConditionRequestValidator()
        {
            RuleFor(x => x.BadgeConditionId).NotEmpty().NotNull();
            RuleFor(x => x.Accomplishments).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Orders).GreaterThanOrEqualTo(0);
        }
    }
}
