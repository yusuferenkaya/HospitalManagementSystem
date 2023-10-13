using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AppointmentHourValidator : AbstractValidator<AppointmentHour>
    {
        public AppointmentHourValidator()
        {
            RuleFor(x => x.Hour).NotEmpty().WithMessage("Hour cannot be empty");

        }
    }
}
