using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AnnualLeaveValidator : AbstractValidator<AnnualLeave>
    {
        public AnnualLeaveValidator()
        {
            RuleFor(x => x.startDate).NotEmpty().WithMessage("Start Date cannot be empty");
            RuleFor(x => x.finishDate).NotEmpty().WithMessage("Finish Date cannot be empty");
            RuleFor(x => x.doctorID).NotEmpty().WithMessage("Doctor ID cannot be empty");
            RuleFor(x => x.finishDate).GreaterThan(x => x.startDate).WithMessage("Start date must be earlier than end date");

        }
    }
}
