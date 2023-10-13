using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.appDate).NotEmpty().WithMessage("App Date cannot be empty");
            RuleFor(x => x.doctorID).NotEmpty().WithMessage("Doctor cannot be empty");
            RuleFor(x => x.patientID).NotEmpty().WithMessage("Patient cannot be empty");

        }
    }
}
