using Application.Common.Constants;
using Application.Common.Validator;
using Application.Features.ProductFeature.Commands;
using FluentValidation;

namespace Application.Features.ProductFeature.Validators
{
    public class UpdateProductCommandNewValidator : ValidatorBase<UpdateProductCommandNew>
    {
        public UpdateProductCommandNewValidator()
        {
            // Valider uniquement si Name est fourni
            When(v => v.Name is not null, () =>
            {
                RuleFor(v => v.Name).NotEmpty()
                    .WithMessage("Le nom du produit est requis.");
            });

            // Valider uniquement si Category est fourni
            When(v => v.Category is not null, () =>
            {
                RuleFor(v => v.Category).NotEmpty()
                    .WithMessage("La catégorie est requise.");
            });

            // Valider uniquement si Description est fournie
            When(v => v.Description is not null, () =>
            {
                RuleFor(v => v.Description).NotEmpty()
                    .WithMessage("La description est requise.");
            });

            // Valider uniquement si Price est défini
            When(v => v.Price != 0, () =>
            {
                RuleFor(v => v.Price).GreaterThan(0)
                    .WithMessage("Le prix doit être supérieur à zéro.");
            });

            // Toujours valider StockQuantity (car non-nullable)
            RuleFor(v => v.StockQuantity).GreaterThanOrEqualTo(0)
                .WithMessage("Le stock ne peut pas être négatif.");
        }
    }
}
