using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request
{
    public class CreateNewCaloRefRequest
    {
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int Calo { get; set; }
        public bool IsMale { get; set; }
    }
    public class CreateNewCaloRefRequestValidator : AbstractValidator<CreateNewCaloRefRequest>
    {
        public CreateNewCaloRefRequestValidator()
        {
            RuleFor(x => x.FromAge).NotEmpty().NotNull();
            RuleFor(x => x.ToAge).NotEmpty().NotNull();
            RuleFor(x => x.Calo).NotEmpty().NotNull();
            RuleFor(x => x.IsMale).NotEmpty().NotNull();
        }
    }
}
