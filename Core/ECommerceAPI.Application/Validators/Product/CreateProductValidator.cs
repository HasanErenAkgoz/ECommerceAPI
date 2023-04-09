using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("please enter the product name").MinimumLength(0).WithMessage("please enter the product name with at least 5 characters");
            RuleFor(x => x.Price).NotEmpty().WithMessage("please enter the price").GreaterThanOrEqualTo(0).WithMessage("please enter the product stock information with greather than or equal");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("please enter the stock ").GreaterThanOrEqualTo(0).WithMessage("please enter the product stock information with greather than or equal");

        }
    }
}
