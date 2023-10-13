using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(x => x.patientPreName).NotEmpty().WithMessage("Patient pre name cannot be empty");
            RuleFor(x => x.patientLastName).NotEmpty().WithMessage("Patient last name cannot be empty");
            RuleFor(x => x.patientBirthDate).NotEmpty().WithMessage("Patient birthdate cannot be empty");
            RuleFor(x => x.patientSocSecNo).NotEmpty().WithMessage("Patient Social Security Number cannot be empty");
            RuleFor(x => x.userID).NotEmpty().WithMessage("Patient phone number cannot be empty");


        }
    }
}