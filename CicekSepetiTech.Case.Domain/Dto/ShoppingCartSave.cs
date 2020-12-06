using CicekSepetiTech.Case.Domain.Validator;
using FluentValidation.Attributes;
using System;

namespace CicekSepetiTech.Case.Domain.Dto
{
    [Validator(typeof(ShoppingCartSaveValidator))]
    public class ShoppingCartSave
    {
        public CustomerInfo CustomerInfo { get; set; }
        public DateTime? DeliveryDateTime { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}