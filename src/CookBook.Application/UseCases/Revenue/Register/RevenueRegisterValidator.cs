using CookBook.Communication.Request;
using FluentValidation;

namespace CookBook.Application.UseCases.Revenue.Register;
public class RevenueRegisterValidator : AbstractValidator<RevenueRequest>
{
    public RevenueRegisterValidator()
    {
        RuleFor(revenue => revenue.Titulo).NotEmpty();
        RuleFor(revenue => revenue.Categoria).IsInEnum();
        RuleFor(revenue => revenue.ModoPreparo).NotEmpty();
        RuleFor(revenue => revenue.Ingredientes).NotEmpty();
        RuleForEach(revenue => revenue.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(x => x.Produto).NotEmpty();
            ingrediente.RuleFor(x => x.Quantidade).NotEmpty();
        });

        RuleFor(revenue => revenue.Ingredientes).Custom((ingredientes, context) =>
        {
            var distinctProducts = ingredientes.Select(x => x.Produto).Distinct();
            if (distinctProducts.Count() != ingredientes.Count())
            {
                context.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", ""));
            }
        });
    }
}
