using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class DepartmentValidator:AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.departmentName).NotEmpty().WithMessage("Department name cannot be empty");
            RuleFor(x => x.locationAdress).NotEmpty().WithMessage("Department location cannot be empty");

        }
    }
}
