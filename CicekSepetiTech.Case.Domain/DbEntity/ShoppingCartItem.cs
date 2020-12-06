using CicekSepetiTech.Case.Domain.DbEntity.Base;
using System;

namespace CicekSepetiTech.Case.Data.DbEntity
{
    public class ShoppingCartItem : BaseEntity<int>
    {
        public int? CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentPriceInclTax { get; set; }
        public DateTime? DeliveryDateTime { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}