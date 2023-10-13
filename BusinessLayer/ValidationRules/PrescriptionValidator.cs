using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PrescriptionValidator : AbstractValidator<Prescription>
    {
        public PrescriptionValidator()
        {
            RuleFor(x => x.medicineName).NotEmpty().WithMessage("Medicine name cannot be empty!");
            RuleFor(x => x.useIntr).NotEmpty().WithMessage("Prescription use instructions cannot be empty");
            RuleFor(x => x.duration).NotEmpty().WithMessage("Duration cannot be empty");
            RuleFor(x => x.appointmentID).NotEmpty().WithMessage("Appointment ID cannot be empty");

        }
    }
}
