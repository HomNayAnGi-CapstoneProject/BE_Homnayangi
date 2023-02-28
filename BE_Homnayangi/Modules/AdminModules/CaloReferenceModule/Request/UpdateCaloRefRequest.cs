using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request
{
    public class UpdateCaloRefRequest
    {
        public Guid CaloRefId { get; set; }
        public int? FromAge { get; set; }
        public int? ToAge { get; set; }
        public int? Calo { get; set; }
        public bool? IsMale { get; set; }
    }
    public class UpdateCaloRefRequestValidator : AbstractValidator<UpdateCaloRefRequest>
    {
        public UpdateCaloRefRequestValidator()
        {
            RuleFor(x => x.CaloRefId).NotEmpty().NotNull();
        }
    }
}
