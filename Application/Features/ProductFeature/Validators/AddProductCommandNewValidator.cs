using Application.Common.Constants;
using Application.Common.Validator;
using Application.Features.ProductFeature.Commands;
using FluentValidation;

namespace Application.Features.ProductFeature.Validators
{
    public class AddProductCommandNewValidator : ValidatorBase<AddProductCommandNew>
    {
        public AddProductCommandNewValidator()
        {
            RuleFor(v => v.name).NotEmpty()
                .WithMessage("Le nom du produit est requis.");

            RuleFor(v => v.category).NotEmpty()
                .WithMessage("La catégorie du produit est requise.");

            RuleFor(v => v.description).NotEmpty()
                .WithMessage("La description est requise.");

            RuleFor(v => v.price).GreaterThan(0)
                .WithMessage("Le prix doit être supérieur à zéro.");

            RuleFor(v => v.stockQuantity).GreaterThanOrEqualTo(0)
                .WithMessage("La quantité en stock ne peut pas être négative.");
        }
    }
}
