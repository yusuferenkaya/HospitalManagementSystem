using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class StockValidator:AbstractValidator<Stock>
    {
        public StockValidator()
        {
            RuleFor(x => x.stockAmount).NotEmpty().WithMessage("Stock amount cannot be empty");
            RuleFor(x => x.stockName).NotEmpty().WithMessage("Stok name cannot be empty");

        }
    }
}
