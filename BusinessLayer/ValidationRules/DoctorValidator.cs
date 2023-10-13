using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            RuleFor(x => x.doctorPreName).NotEmpty().WithMessage("Doctor pre name cannot be empty");
            RuleFor(x => x.doctorLastName).NotEmpty().WithMessage("Doctor last name cannot be empty");
            RuleFor(x => x.birthDate).NotEmpty().WithMessage("Doctor birthdate cannot be empty");
            RuleFor(x => x.salary).NotEmpty().WithMessage("Doctor salary cannot be empty");


        }
    }
}