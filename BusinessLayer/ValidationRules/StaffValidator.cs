using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(x => x.staffPreName).NotEmpty().WithMessage("Staff pre name cannot be empty");
            RuleFor(x => x.staffLastName).NotEmpty().WithMessage("Staff last name cannot be empty");
            RuleFor(x => x.staffSalary).NotEmpty().WithMessage("Staff salary cannot be empty");
            RuleFor(x => x.stafSocSecNo).NotEmpty().WithMessage("Staff Social Security Number cannot be empty");
            RuleFor(x => x.staffTask).NotEmpty().WithMessage("Staff task cannot be empty");


        }
    }
}