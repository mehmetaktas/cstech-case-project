using CicekSepetiTech.Case.Domain.Dto;
using FluentValidation;

namespace CicekSepetiTech.Case.Domain.Validator
{
    public class ShoppingCartSaveValidator : AbstractValidator<ShoppingCartSave>
    {
        public ShoppingCartSaveValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Boş geçilemez!").NotEqual(0).WithMessage("0 olamaz!");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Boş geçilemez!").NotEqual(0).WithMessage("0 olamaz!");
            RuleFor(x => x.CustomerInfo).NotNull().NotEmpty().WithMessage("Boş geçilemez!");
            RuleFor(x => x.CustomerInfo.CustomerId).NotEqual(0).WithMessage("0 olamaz!");

            //CustomerCode ya da CustomerId alanlarından sadece birisi dolu olabilir!
            When(x => x.CustomerInfo.CustomerId.HasValue && x.CustomerInfo.CustomerId != 0, () =>
            {
                RuleFor(x => x.CustomerInfo.CustomerCode).Empty().WithMessage("CustomerId ve CustomerCode aynı anda dolu olamaz!");
            });
            When(x => !string.IsNullOrEmpty(x.CustomerInfo.CustomerCode), () =>
            {
                RuleFor(x => x.CustomerInfo.CustomerId).Empty().WithMessage("CustomerId ve CustomerCode aynı anda dolu olamaz!");
            });
        }
    }
}