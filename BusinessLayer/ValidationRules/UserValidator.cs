using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.userPreName).NotEmpty().WithMessage("User pre name cannot be empty");
            RuleFor(x => x.userLastName).NotEmpty().WithMessage("User last name cannot be empty");
            RuleFor(x => x.userEmail).NotEmpty().WithMessage("User email cannot be empty");
            RuleFor(x => x.userPassword).NotEmpty().WithMessage("User password cannot be empty");


        }
    }
}